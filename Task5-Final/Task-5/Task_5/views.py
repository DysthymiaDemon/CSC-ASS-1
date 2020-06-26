"""
Routes and views for the flask application.
"""

from datetime import datetime
from flask import render_template, render_template, redirect, jsonify, make_response, request
from Task_5 import app
import requests 
import boto3
from botocore.exceptions import NoCredentialsError
import pathlib
import os
import json

#Keys for AWS S3 Bucket
ACCESS_KEY = ''
SECRET_KEY = ''

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

@app.route('/task5')
def task5():
    """Renders the about page."""
    return render_template(
        'task-5.html',
        title='Task5',
        year=datetime.now().year,
        message='Task5 Demo'
    )


@app.route('/task5/upload', methods=["POST"])
def upload():
    s3 = boto3.client('s3', aws_access_key_id=ACCESS_KEY,
                      aws_secret_access_key=SECRET_KEY)
    local_file = os.path.normpath( 'C:/Users/2014p/CSC-ASS-1/Task5-Final/Task-5/Task_5/images/'+ request.form['file']);
    bucket =  'bucketdemotwo'
    s3_file = request.form['file']
    _bitlyToken = '560c63bf3029b375974070eea7ebaf4042c8782a'
    API_ENDPOINT = 'https://api-ssl.bitly.com/v4/shorten'

    try:
        s3.upload_file(local_file, bucket, s3_file, ExtraArgs={'ACL': 'public-read'})
        print("Upload Successful")
        url = 'https://bucketdemotwo.s3-ap-southeast-1.amazonaws.com/'+request.form['file']
  
        # data to be sent to api 
        data = {'long_url': url} 
        headers = {'Authorization': 'Bearer '+_bitlyToken, 'Content-type': 'application/json'}

        # sending post request and saving response as response object 
        try:
            res = requests.post(url = API_ENDPOINT, json = data, headers=headers)
            print(res.text)

            return json.dumps(res.json())
        except:
            print("Bitlink POST error")
            return jsonify({"error": "The URL cannot be shortened"})

    except FileNotFoundError:
        print("The file was not found")
        return jsonify({"error": "The file was not found"})
    except NoCredentialsError:
        print("Credentials not available")
        return jsonify({"error": "Credentials not available"})
