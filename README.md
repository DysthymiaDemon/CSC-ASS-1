# CSC-ASS-1
Creating and Consuming Web API 

## Table of Contents 

- [Introduction](#introduction)
- [Diagrams](#diagram)
- [Web API Documents](#documents)
- [Team](#team)

## Diagram
#### Task 1 Diagram
[Task 1 Diagram](https://github.com/MyridianStar/CSC-ASS-1/blob/master/Diagrams/Task1.pdf)<br/>

#### Task 3 Sequence Diagram
![Task 3 Sequence Diagram](https://github.com/MyridianStar/CSC-ASS-1/blob/master/Diagrams/Task3.jpg)

#### Task 4 Sequence Diagram
![Task 4 Sequence Diagram](https://github.com/MyridianStar/CSC-ASS-1/blob/master/Diagrams/Task4.jpg)

#### Task 5 Sequence Diagram
![Task 5 Sequence Diagram](https://github.com/MyridianStar/CSC-ASS-1/blob/master/Diagrams/Task5.jpg)

#### Task 6 Sequence Diagram
![Task 6 Sequence Diagram](https://github.com/MyridianStar/CSC-ASS-1/blob/master/Diagrams/Task6.jpg)

#### Task 7 Diagram
![Task 7 Diagram](https://github.com/MyridianStar/CSC-ASS-1/blob/master/Diagrams/Task7%20Diagram.png)

#### Task 8 Sequence Diagram
![Task 8 Sequence Diagram](https://github.com/MyridianStar/CSC-ASS-1/blob/master/Diagrams/Task8.jpg)

## Web API Documents

### Task 2

>#### **GET** /api/v1/students
Description: Get all Students

URI Parameters: None

Body Parameters: None

Response : 200 OK 
[
    {
        "Id": 1,
        "Name": "Philip",
        "Year": 3,
        "Course": "Diploma in Information Technology",
        "Email": "philip@gmail.com"
    },
    {
        "Id": 2,
        "Name": "Ameen",
        "Year": 1,
        "Course": "Diploma in Information Technology",
        "Email": "ameen@gmail.com"
    },
    {
        "Id": 3,
        "Name": "Darius",
        "Year": 2,
        "Course": "Diploma in Information Technology",
        "Email": "darius@gmail.com"
    },
    {
        "Id": 4,
        "Name": "Gary",
        "Year": 3,
        "Course": "Diploma in Business Information Technology",
        "Email": "gary@gmail.com"
    },
    {
        "Id": 5,
        "Name": "Zion",
        "Year": 2,
        "Course": "Diploma in Business Information Technology",
        "Email": "zion@gmail.com"
    }
]

>#### **GET** /api/v1/students/{id}
Description: Get One Student By Id

URI Parameters: None

Body Parameters: None

Response : 200 OK 
{
    "Id": 3,
    "Name": "Darius",
    "Year": 2,
    "Course": "Diploma in Information Technology",
    "Email": "darius@gmail.com"
}

>#### **GET** /api/v1/students?course={course}
Description: Get All Students In Specified Course

URI Parameters: None

Body Parameters: None

Response : 200 OK 
[
    {
        "Id": 1,
        "Name": "Philip",
        "Year": 3,
        "Course": "Diploma in Information Technology",
        "Email": "philip@gmail.com"
    },
    {
        "Id": 2,
        "Name": "Ameen",
        "Year": 1,
        "Course": "Diploma in Information Technology",
        "Email": "ameen@gmail.com"
    },
    {
        "Id": 3,
        "Name": "Darius",
        "Year": 2,
        "Course": "Diploma in Information Technology",
        "Email": "darius@gmail.com"
    }
]


>#### **POST** /api/v1/students
Description: Post 1 Student

URI Parameters: None

Body Parameters: 

Name | Variable | Required
------------ | ------------- | ------------- 
Name | [String] | **Required**
Year | [Int] | **Required**
Course | [String] | **Required**
Email | [String] | **Required**

Response : 201 Created
{
    "Id": 6,
    "Name": "CreateStudent",
    "Year": 1,
    "Course": "Diploma in Business Information Technology",
    "Email": "NewStudent@hotmail.com"
}

>#### **PUT** /api/v1/students/{id}
Description: Update 1 Student

URI Parameters: 
Name | Variable | Required
------------ | ------------- | ------------- 
Id | [Int] | **Required**

Body Parameters: 

Name | Variable | Required
------------ | ------------- | ------------- 
Id | [Int] | None
Name | [String] | **Required**
Year | [Int] | **Required**
Course | [String] | **Required**
Email | [String] | **Required**

Response : 200 OK
{
    "Id": 6,
    "Name": "CreateStudent",
    "Year": 1,
    "Course": "Diploma in Business Information Technology",
    "Email": "NewStudent@hotmail.com"
}

>#### **DELETE** /api/v1/students/{id}
Description: Delete 1 Student

URI Parameters: 
Name | Variable | Required
------------ | ------------- | ------------- 
Id | [Int] | **Required**

Body Parameters: None

Response : 200 OK
{
    "Id": 6,
    "Name": "CreateStudent",
    "Year": 1,
    "Course": "Diploma in Business Information Technology",
    "Email": "NewStudent@hotmail.com"
}


### Task 3

>#### **POST** /api/Account/Verify
Description: Verify registration with ReCaptcha V3

URI Parameters: None

Body Parameters: 

Name | Variable | Required
------------ | ------------- | ------------- 
Email | [String] | **Required**
Password | [String] | **Required**
ConfirmPassword | [String] | **Required**
Recaptcha | [String] | **Required**

Response : 200 OK
{
    true
}

### Task 4

>#### **GET** /api/v1/talents
Description: Get All Talent Entries From Repository

URI Parameters: None

Body Parameters: None

Response : 200 OK 
[
    {
        "Id": 0,
        "Name": "Barot Bellingham",
        "ShortName": "Barot_Bellingham",
        "Reknown": "Royal Academy of Painting and Sculpture",
        "Bio": "Barot has just finished his final year at The Royal Academy of Painting and Sculpture, where he excelled in glass etching paintings and portraiture. Hailed as one of the most diverse artists of his generation, Barot is equally as skilled with watercolors as he is with oils, and is just as well-balanced in different subject areas. Barot's collection entitled \"The Un-Collection\" will adorn the walls of Gilbert Hall, depicting his range of skills and sensibilities - all of them, uniquely Barot, yet undeniably different"
    },
    {
        "Id": 1,
        "Name": "Jonathan G. Ferrar II",
        "ShortName": "Jonathan_Ferrar",
        "Reknown": "Artist to Watch in 2012",
        "Bio": "The Artist to Watch in 2012 by the London Review, Johnathan has already sold one of the highest priced-commissions paid to an art student, ever on record. The piece, entitled Gratitude Resort, a work in oil and mixed media, was sold for $750,000 and Jonathan donated all the proceeds to Art for Peace, an organization that provides college art scholarships for creative children in developing nations"
    },
    {
        "Id": 2,
        "Name": "Hillary Hewitt Goldwynn-Post",
        "ShortName": "Hillary_Goldwynn",
        "Reknown": "New York University",
        "Bio": "Hillary is a sophomore art sculpture student at New York University, and has already won all the major international prizes for new sculptors, including the Divinity Circle, the International Sculptor's Medal, and the Academy of Paris Award. Hillary's CAC exhibit features 25 abstract watercolor paintings that contain only water images including waves, deep sea, and river."
    },
    {
        "Id": 3,
        "Name": "Hassum Harrod",
        "ShortName": "Hassum_Harrod",
        "Reknown": "Art College in New Dehli",
        "Bio": "The Art College in New Dehli has sponsored Hassum on scholarship for his entire undergraduate career at the university, seeing great promise in his contemporary paintings of landscapes - that use equal parts muted and vibrant tones, and are almost a contradiction in art. Hassum will be speaking on \"The use and absence of color in modern art\" during Thursday's agenda."
    }
]

>#### **GET** api/v1/talents/{id}
Description: Get Specific Talent Entry From Repository

URI Parameters: None

Body Parameters: None

Response : 200 OK 
{
    "Id": 1,
    "Name": "Jonathan G. Ferrar II",
    "ShortName": "Jonathan_Ferrar",
    "Reknown": "Artist to Watch in 2012",
    "Bio": "The Artist to Watch in 2012 by the London Review, Johnathan has already sold one of the highest priced-commissions paid to an art student, ever on record. The piece, entitled Gratitude Resort, a work in oil and mixed media, was sold for $750,000 and Jonathan donated all the proceeds to Art for Peace, an organization that provides college art scholarships for creative children in developing nations"
}
## Team 
| <a href="https://github.com/PhilipLeong" target="_blank">`Philip Leong`</a> | <a href="https://github.com/MyridianStar" target="_blank">`Ameen Khan`</a> |
---
