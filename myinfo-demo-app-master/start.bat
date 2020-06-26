@ECHO off
set DEMO_APP_SIGNATURE_CERT_PRIVATE_KEY=./ssl/stg-demoapp-client-privatekey-2018.pem
set MYINFO_SIGNATURE_CERT_PUBLIC_CERT=./ssl/staging_myinfo_public_cert.cer

set MYINFO_APP_CLIENT_ID=STG2-MYINFO-SELF-TEST
set MYINFO_APP_CLIENT_SECRET=44d953c796cccebcec9bdc826852857ab412fbe2
set MYINFO_APP_REDIRECT_URL=http://localhost:3001/callback

rem SANDBOX ENVIRONMENT (no PKI digital signature)
rem set AUTH_LEVEL=L0
rem set MYINFO_API_AUTHORISE=https://sandbox.api.myinfo.gov.sg/com/v3/authorise
rem set MYINFO_API_TOKEN=https://sandbox.api.myinfo.gov.sg/com/v3/token
rem set MYINFO_API_PERSON=https://sandbox.api.myinfo.gov.sg/com/v3/person

rem TEST ENVIRONMENT (with PKI digital signature)
set AUTH_LEVEL=L2
set MYINFO_API_AUTHORISE=https://test.api.myinfo.gov.sg/com/v3/authorise
set MYINFO_API_TOKEN=https://test.api.myinfo.gov.sg/com/v3/token
set MYINFO_API_PERSON=https://test.api.myinfo.gov.sg/com/v3/person

npm start