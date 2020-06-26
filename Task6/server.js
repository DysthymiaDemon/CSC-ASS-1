const express = require('express');
const app = express();
const { resolve } = require('path');
const bodyParser = require('body-parser');

// Get env file for keys and Ids
const dotenv = require('dotenv');
dotenv.config();

app.use(express.static('view'));

// Stripe
const stripe = require('stripe')(process.env.STRIPE_SECRET_KEY);

var server = app.listen(process.env.PORT, function () {
    var port = server.address().port;
    console.log('Web App Hosted at http://localhost:%s/index.html', port);
});

// Use JSON parser for all non-webhook routes
app.use((req, res, next) => {
  if (req.originalUrl === '/stripe-webhook') {
    next();
  } else {
    bodyParser.json()(req, res, next);
  }
});

app.get('/', (req, res) => {
  const path = resolve(process.env.STATIC_DIR + '/index.html');
  res.sendFile(path);
});

app.get('/config', async (req, res) => {
  res.send({
    publishableKey: process.env.STRIPE_PUBLISHABLE_KEY,
  });
});

app.post('/create-customer', async (req, res) => {
  // Create a new customer object
  const customer = await stripe.customers.create({
    email: req.body.email,
  });
  res.send({ customer });
});

app.post('/create-subscription', async (req, res) => {
  // Set the default payment method on the customer
  try {
    await stripe.paymentMethods.attach(req.body.paymentMethodId, {
      customer: req.body.customerId,
    });
  } catch (error) {
    return res.status('402').send({ error: { message: error.message } });
  }
try{
  await stripe.customers.update(
    req.body.customerId,
    {
      invoice_settings: {
        default_payment_method: req.body.paymentMethodId,
      },
    }
  );
}catch (error) {
  return res.status('402').send({ error: { message: error.message } });
}
  // Create the subscription
  const subscription = await stripe.subscriptions.create({
    customer: req.body.customerId,
    items: [{ price: process.env[req.body.priceId] }],
    expand: ['latest_invoice.payment_intent'],
  });

  res.send(subscription);
});

app.post('/retry-invoice', async (req, res) => {
  // Set the default payment method on the customer

  try {
    await stripe.paymentMethods.attach(req.body.paymentMethodId, {
      customer: req.body.customerId,
    });
    await stripe.customers.update(req.body.customerId, {
      invoice_settings: {
        default_payment_method: req.body.paymentMethodId,
      },
    });
  } catch (error) {
    // in case card_decline error
    return res
      .status('402')
      .send({ result: { error: { message: error.message } } });
  }

  const invoice = await stripe.invoices.retrieve(req.body.invoiceId, {
    expand: ['payment_intent'],
  });
  res.send(invoice);
});

app.post('/retrieve-upcoming-invoice', async (req, res) => {
  const subscription = await stripe.subscriptions.retrieve(
    req.body.subscriptionId
  );
  const invoice = await stripe.invoices.retrieveUpcoming({
    subscription_prorate: true,
    customer: req.body.customerId,
    subscription: req.body.subscriptionId,
    subscription_items: [
      {
        id: subscription.items.data[0].id,
        deleted: true,
      },
      {
        price: process.env[req.body.newPriceId],
        deleted: false,
      },
    ],
  });
  res.send(invoice);
});

 app.post('/manage-billing', async (req, res) =>{
   // redirect to customer billing portal hosted by Stripe
    try{
      stripe.billingPortal.sessions.create(
        {
          customer: req.body.customerId,
          return_url: req.body.return,
        },
        function(err, session) {
          // asynchronously called
          res.send(session);
        }
      );
    }catch (error) {
    return res.status('402').send({ error: { message: error.message } });
  }
  }
);
app.post('/cancel-subscription', async (req, res) => {
  // Delete the subscription
  const deletedSubscription = await stripe.subscriptions.del(
    req.body.subscriptionId
  );
  res.send(deletedSubscription);
});

app.post('/pause-subscription', async (req, res) => {
  // Pause the subscription
  const subscription = await stripe.subscriptions.update(
    req.body.subscriptionId,
    {
      pause_collection: {
        behavior: 'mark_uncollectible',
      },
    }
  );
  res.send(subscription);
});

app.post('/resume-subscription', async (req, res) => {
  // Resume the subscription
  const subscription = await stripe.subscriptions.update(
    req.body.subscriptionId,
    {
      pause_collection: '',
    }
  );
  res.send(subscription);
});

app.post('/refund-subscription', async (req, res) => {
  // Refund the subscription
  const refund = await stripe.refunds.create({
    payment_intent: req.body.paymentIntentId,
  });
  res.send(refund);
});


app.post('/update-subscription', async (req, res) => {
  const subscription = await stripe.subscriptions.retrieve(
    req.body.subscriptionId
  );
  const updatedSubscription = await stripe.subscriptions.update(
    req.body.subscriptionId,
    {
      cancel_at_period_end: false,
      items: [
        {
          id: subscription.items.data[0].id,
          price: process.env[req.body.newPriceId],
        },
      ],
    }
  );

  res.send(updatedSubscription);
});

app.post('/retrieve-customer-payment-method', async (req, res) => {
  const paymentMethod = await stripe.paymentMethods.retrieve(
    req.body.paymentMethodId
  );

  res.send(paymentMethod);
});

// Webhook handler for asynchronous events.
app.post(
  '/stripe-webhook',
  bodyParser.raw({ type: 'application/json' }),
  async (req, res) => {
    let event;

    try {
      event = stripe.webhooks.constructEvent(
        req.body,
        req.headers['stripe-signature'],
        process.env.STRIPE_WEBHOOK_SECRET
      );
    } catch (err) {
      console.log(err);
      console.log(`⚠️  Webhook signature verification failed.`);
      console.log(
        `⚠️  Check the env file and enter the correct webhook secret.`
      );
      return res.sendStatus(400);
    }
    const dataObject = event.data.object;

    switch (event.type) {
      case 'invoice.payment_succeeded':

        break;
      case 'invoice.payment_failed':

        break;
      case 'invoice.finalized':

        break;
      case 'customer.subscription.deleted':
        if (event.request != null) {

        } else {

        }
        break;
      case 'customer.subscription.trial_will_end':

        break;
      default:
    }
    res.sendStatus(200);
  }
);
