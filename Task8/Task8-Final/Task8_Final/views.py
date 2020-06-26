#!/usr/bin/python
# -*- coding: utf-8 -*-

"""
Routes and views for the flask application.
"""

from datetime import datetime
from flask import render_template, render_template, redirect, jsonify, make_response, request
from Task8_Final import app
from pathlib import Path
import dialogflow_v2 as dialogflow
import os

import requests
import pprint

from clarifai.rest import ClarifaiApp

from veryfi import Client
appClarifai = ClarifaiApp(api_key='')


@app.route('/')
@app.route('/home')
def home():
    """Renders the home page."""

    return render_template('index.html', title='Home Page',
                           year=datetime.now().year)


@app.route('/contact')
def contact():
    """Renders the contact page."""

    return render_template('contact.html', title='Contact',
                           year=datetime.now().year,
                           message='Your contact page.')


@app.route('/about')
def about():
    """Renders the about page."""

    return render_template('about.html', title='About',
                           year=datetime.now().year,
                           message='Your application description page.')


@app.route('/clarifai')
def clarifai():
    """Renders the clarifai page."""

    return render_template('clarifai.html', title='Clarifai',
                           year=datetime.now().year,
                           message='Clarifai Demo')


@app.route('/uploadurl')
def updateurl():
    """Renders the clarifai page."""

    return render_template('uploadurl.html', title='Upload by URL',
                           year=datetime.now().year,
                           message='Upload by URL Demo')


@app.route('/veryfi')
def veryfi():
    """Renders the clarifai page."""

    return render_template('veryfi.html', title='Receipt Amount',
                           year=datetime.now().year,
                           message='Get Receipt Amount Demo')


@app.route('/predicturl')
def predicturl():
    """Renders the clarifai page."""

    return render_template('predicturl.html',
                           title='Predict Receipt by URL',
                           year=datetime.now().year,
                           message='Predict Receipt by URL Demo')


# Process URL and get Tags

@app.route('/uploadurl/processTags', methods=['POST'])
def processTags():
    output = ''
    try:
        req = request.form['url']

        # Clarifai API

        try:
            model = appClarifai.public_models.general_model
            responseFromClarifai = model.predict_by_url(req)
        except:
            return jsonify({'error': 'Please enter a valid URL'})

        for tag in responseFromClarifai['outputs'][0]['data']['concepts'
                ]:
            output += tag['name'] + '\n'

        print(output)
        return output
    except:
        return jsonify({'error': 'Error in processing request'})


# Process Upload of Image

@app.route('/processUploadURL', methods=['POST'])
def processUpload():
    try:
        req = request.form['url']

        try:
            appClarifai.inputs.create_image_from_url(req)
        except:
            return jsonify({'error': 'Input already exists'})

        print('Upload Successful')
        return 'Upload Successful'
    except:

        return jsonify({'error': 'Error in processing request'})


# Get amount listed in the receipt

@app.route('/veryfi/getamount', methods=['POST'])
def getamount():

        # Veryfi API Details

    CLIENT_ID = ''
    ENVIRONMENT_URL = 'api.veryfi.com'
    username = ''
    api_key = ''
    process_image_url = \
        'https://{0}/api/v7/partner/documents/'.format(ENVIRONMENT_URL)
    headers = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'CLIENT-ID': CLIENT_ID,
        'AUTHORIZATION': 'apikey {0}:{1}'.format(username, api_key),
        }

        # Google Credentials for DialogFlow API

    os.environ['GOOGLE_APPLICATION_CREDENTIALS'] = \
        os.path.abspath(''
                        )

        # Call Veryfi API

    try:
        file_url = request.form['url']
        file_name = Path(file_url).name
        payload = {'file_name': file_name, 'file_url': file_url}

        try:
            response = requests.post(url=process_image_url,
                    headers=headers, json=payload)

                # Beautify Printed Response

            try:
                pp = pprint.PrettyPrinter(indent=2)
                pp.pprint(response.json())
            except:

                return jsonify({'error': 'Internal server error'})

                # Declare output for Client

            output = response.json()['total']

                # DialogFlow API Details

            try:
                project_id = ''
                session_id = '12345'
                texts = 'The price is ' + str(response.json()['total'])
                language_code = 'en-US'

                session_client = dialogflow.SessionsClient()

                session_path = session_client.session_path(project_id,
                        session_id)
                print('Session path: {}\n'.format(session_path))

                text_input = dialogflow.types.TextInput(text=texts,
                        language_code=language_code)

                query_input = \
                    dialogflow.types.QueryInput(text=text_input)

                    # Set the query parameters with sentiment analysis

                output_audio_config = \
                    dialogflow.types.OutputAudioConfig(audio_encoding=dialogflow.enums.OutputAudioEncoding.OUTPUT_AUDIO_ENCODING_LINEAR_16)

                response = \
                    session_client.detect_intent(session=session_path,
                        query_input=query_input,
                        output_audio_config=output_audio_config)

                print('=' * 20)
                print('Query text: {}'.format(response.query_result.query_text))
                print('Detected intent: {} (confidence: {})\n'.format(response.query_result.intent.display_name,
                        response.query_result.intent_detection_confidence))
                print('Fulfillment text: {}\n'.format(response.query_result.fulfillment_text))

                    # Output Audio File

                with open('output.wav', 'wb') as out:
                    out.write(response.output_audio)
                    print('Audio content written to file "output.wav"')
            except:
                return jsonify({'error': 'Internal Server Error'})

                # Return output

            return str(output)
        except:

            return jsonify({'error': 'Please enter a valid URL'})
    except:
        return jsonify({'error': 'Error'})


# Predict if the URL Image is a Receipt

@app.route('/predicturl/processPredict', methods=['POST'])
def processPredict():
    output = ''
    try:
        req = request.form['url']

        # Clarifai API

        try:

            # Custom Model

            model = appClarifai.models.get('receipts')
            response = model.predict_by_url(req)
        except:
            return jsonify({'error': 'Please enter a valid URL'})

        if response['outputs'][0]['data']['concepts'][0]['value'] < 0.5:
            output = 'Not a receipt!'
        elif response['outputs'][0]['data']['concepts'][0]['value'] \
            > 0.5:
            output = 'It is a receipt!'
        else:
            output = 'Error'

        return output
    except:

        return jsonify({'error': 'Error in processing request'})
