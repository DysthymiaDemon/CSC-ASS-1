"""
Routes and views for the flask application.
"""

from datetime import datetime
from flask import render_template, render_template, redirect, jsonify, make_response, request
from Task8_Final import app
from pathlib import Path  

import requests
import pprint

from clarifai.rest import ClarifaiApp


from veryfi import Client
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

@app.route('/veryfi')
def veryfi():
    """Renders the clarifai page."""
    return render_template(
        'veryfi.html',
        title='Receipt Amount',
        year=datetime.now().year,
        message='Get Receipt Amount Demo'
    )

@app.route('/veryfi/getamount', methods=["POST"])

def getamount():
        CLIENT_ID = 'vrfcubWWckvPac3avIzhd8keEEG5atcBVnupysx'
        ENVIRONMENT_URL = "api.veryfi.com"

        username = "2014.philipleongjunhwa"
        api_key = "37330e9bed86bad37528fcdbd916b575"
        process_image_url = 'https://{0}/api/v7/partner/documents/'.format(ENVIRONMENT_URL)
        headers = {
            "Content-Type": "application/json",
            "Accept": "application/json", 
            "CLIENT-ID": CLIENT_ID,
            "AUTHORIZATION": "apikey {0}:{1}".format(username, api_key)
        }
         
        try:
            file_url = request.form['url']
            file_name =  Path(file_url).name
            payload = {'file_name': file_name,'file_url': file_url}

            try:
                response = requests.post(url=process_image_url, headers=headers, json=payload)

                try:
                    pp = pprint.PrettyPrinter(indent=2)
                    pp.pprint(response.json())

                except: 
                    return jsonify({"error": "Internal server error"})

                return str(response.json()['total'])
            except:
                return jsonify({"error": "Please enter a valid URL"})
        except:
            return jsonify({"error": "Error"})

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

