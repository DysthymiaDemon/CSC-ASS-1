# CSC-ASS-1
Creating and Consuming Web API 

## Table of Contents 

- [Introduction](#introduction)
- [Diagrams](#diagram)
- [Web API Documents](#documents)
- [Team](#team)

## Diagram

## Documents

### Task 1

#### **GET** api/v1/students
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

#### **POST** api/v1/students
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




### Task 2


## Team

> Or Contributors/People

| <a href="http://fvcproductions.com" target="_blank">**FVCproductions**</a> | <a href="http://fvcproductions.com" target="_blank">**FVCproductions**</a> | <a href="http://fvcproductions.com" target="_blank">**FVCproductions**</a> |
| :---: |:---:| :---:|
| [![FVCproductions](https://avatars1.githubusercontent.com/u/4284691?v=3&s=200)](http://fvcproductions.com)    | [![FVCproductions](https://avatars1.githubusercontent.com/u/4284691?v=3&s=200)](http://fvcproductions.com) | [![FVCproductions](https://avatars1.githubusercontent.com/u/4284691?v=3&s=200)](http://fvcproductions.com)  |
| <a href="http://github.com/fvcproductions" target="_blank">`github.com/fvcproductions`</a> | <a href="http://github.com/fvcproductions" target="_blank">`github.com/fvcproductions`</a> | <a href="http://github.com/fvcproductions" target="_blank">`github.com/fvcproductions`</a> |

- You can just grab their GitHub profile image URL
- You should probably resize their picture using `?s=200` at the end of the image URL.

---
