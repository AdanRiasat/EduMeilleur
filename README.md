# EduMeilleur

[Short tagline or slogan]

## 📘 Description

ÉduMeilleur is an educational platform designed for secondary school students. Many students struggle with the advanced courses they’re required to take to pursue their career goals. Finding help can be challenging, tutors are expensive, support from teachers may not always be enough, and students often lack access to sufficient practice materials. Our platform addresses all of these issues.

![home](readme-images/home.png)

After selecting a subject, students can access detailed written notes, educational videos, and a revision cheat sheet for each chapter —all in one place. They’ll also find a variety of exercises tied to each chapter, complete with answers for self-assessment. A dedicated video page offers extra resources, including important explanations, helpful insights, and study tips.

![subjects](readme-images/subjects.png)
![subject](readme-images/sn4.png)

Students can also reach out to us via the contact page to ask questions. They can include images or attachments, and our team of teachers will respond with reliable answers.

![contact-us](readme-images/contactus.png)

Additionally, the site features an AI chatbot to assist with quick and simple questions.

![chatbot](readme-images/Chatbot.png)

Our Explore page, currently in development, will allow students and teachers to share notes and exercises with the community.

## 🌐 Live Website

Hosted on [RaspPi_WebHost](https://github.com/AdanRiasat/RaspPi_WebHost), currently only on LAN

Will be fully published soon.

## 🚀 Features

- Notes, exercises, and videos served with markdown files sent by the backend.
- 2 carrousels on the home page, one with a swipe functionality and the other with arrows.
- Loading screen during API calls with ngx-spinner.
- Chatbot uses an OpenRouter API key.
- Users can send images and attachments along side their question on the Contact us page.
- Website will be fully translated in english and french (not implemented yet)

## 🛠️ Tech Stack

- Frontend: Angular
- Backend: ASP.NET CORE
- Database: PostgreSQL
- Hosting: NGINX, Raspberry Pi 4

## 📚 Lessons Learned

First time working on a full stacked Web app from scratch, it feels very different compared to working on an existing project and only working on some of the functionnalities. 

The start of the project felt strange, there were so many things to do, so many functionalities that I was able to add very quickly because there was nothing done, but it also took a while to get everything setup initialy: The backend, frontend, Nuget packages, database, etc. 

Through out the project I found myself doubting and rethinking about a lot of things, like is this the best way to do this? Or is this the standard way of doing it? I had to always be thinking of performance, cleanliness of the code (DRY code) and security. You dont really think about these things as much during school projects since all im focused on is getting all the points, getting the best grade possible, but here everything matters because its my own project and im responsible for it.

Theres also the size of the project, there always seems to be no end for things I need to add and improve. I can defenitly see why you're supposed to have whole team working on this sort of project.

## 📝TODO

- Replace all placeholders
- Create real notes 
- Add error messages to edit-profile
- Make more tests on the backend
- Replace hardcoded teacher pfps with real ones dynamicaly
- Add logo
- Explore page
- Improve nav-bar design
- Fix infinite loading screen when server is off
- Translate website to english and french
- Disconnect user when token is expired
- Add multiple sign up options