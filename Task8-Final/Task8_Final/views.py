"""
Routes and views for the flask application.
"""

from datetime import datetime
from flask import render_template, render_template, redirect, jsonify, make_response, request
from Task8_Final import app

from clarifai.rest import ClarifaiApp

appClarifai = ClarifaiApp(api_key='783bd3bf77d94d37b14b7a069bae86b2')

@app.route('/')
@app.route('/home')
def home():
    """Renders the home page."""
    return render_template(
        'index.html',
        title='Home Page',
        year=datetime.now().year,
    )

@app.route('/contact')
def contact():
    """Renders the contact page."""
    return render_template(
        'contact.html',
        title='Contact',
        year=datetime.now().year,
        message='Your contact page.'
    )

@app.route('/about')
def about():
    """Renders the about page."""
    return render_template(
        'about.html',
        title='About',
        year=datetime.now().year,
        message='Your application description page.'
    )

@app.route('/clarifai')
def clarifai():
    """Renders the clarifai page."""
    return render_template(
        'clarifai.html',
        title='Clarifai',
        year=datetime.now().year,
        message='Clarifai Demo'
    )



@app.route('/uploadbytes')
def uploadbytes():
    """Renders the clarifai page."""
    return render_template(
        'uploadbytes.html',
        title='Upload by File',
        year=datetime.now().year,
        message='Upload by File Demo'
    )

@app.route('/uploadurl/processTags', methods=["POST"])
def processTags():
    output = ""
    try:
        req = request.form['url']
        try:
            model = appClarifai.public_models.general_model
            responseFromClarifai = model.predict_by_url(req)
        except:
            return jsonify({"error": "Please enter a valid URL"})

        for tag in responseFromClarifai['outputs'][0]['data']['concepts']:
            output += tag['name']+"\n"
        return output
    except:
        return jsonify({"error": "Error in processing request"})


@app.route('/processUploadURL', methods=["POST"])
def processUpload():
    try:
        req = request.form['url']

        try:
            appClarifai.inputs.create_image_from_url(req)
        except:
            return jsonify({"error": "Input already exists"})

        return "Upload Successful"

    except:
        return jsonify({"error": "Error in processing request"})


@app.route('/uploadurl')
def updateurl():
    """Renders the clarifai page."""
    return render_template(
        'uploadurl.html',
        title='Upload by URL',
        year=datetime.now().year,
        message='Upload by URL Demo'
    )



@app.route('/predictbytes')
def predictbytes():
    """Renders the clarifai page."""
    return render_template(
        'predictbytes.html',
        title='Predict Receipt by File',
        year=datetime.now().year,
        message='Predict Receipt by File Demo'
    )



@app.route('/predicturl')
def predicturl():
    """Renders the clarifai page."""
    return render_template(
        'predicturl.html',
        title='Predict Receipt by URL',
        year=datetime.now().year,
        message='Predict Receipt by URL Demo'
    )

@app.route('/predicturl/processPredict', methods=["POST"])
def processPredict():
    output = ""
    try:
        req = request.form['url']

        try:
            model = appClarifai.models.get('receipts')
            response = model.predict_by_url(req)
        except:
            return jsonify({"error": "Please enter a valid URL"})

        if(response['outputs'][0]['data']['concepts'][0]['value'] < 0.5):
            output = "Not a receipt!"
        elif(response['outputs'][0]['data']['concepts'][0]['value'] > 0.5):
            output = "It is a receipt!"
        else:
            output = "Error"

        return output

    except:
        return jsonify({"error": "Error in processing request"})

