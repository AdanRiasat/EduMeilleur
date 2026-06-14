# EduMeilleur

Pour devenir meilleur, utilisez ÉduMeilleur

## 🌐 Live Website

Hosted on [RaspPi_WebHost](https://github.com/AdanRiasat/RaspPi_WebHost) with Cloudflared tunnels

Link : https://edumeilleur.ca

## Description

ÉduMeilleur is an educational platform designed for secondary school students. Many students struggle with the advanced courses they’re required to take to pursue their career goals. Finding help can be challenging, tutors are expensive, support from teachers may not always be enough, and students often lack access to sufficient practice materials. Our platform addresses all of these issues.

## Features

- Notes, exercises, and videos served with markdown files provided by the backend.

- Two carousels on the home page: one with swipe functionality and the other with navigation arrows.

- Loading screen during API calls using ngx-spinner.

- Users can send images and attachments along with their questions on the Contact Us page.

- Website will be fully translated into English and French (translation not yet implemented).

### Email service

Email service powered by Brevo for automatically sending notifications to users and staff. Custom email templates are built using HTML and CSS.

### Chatbot

AI chatbot powered by the OpenRouter API, supporting multiple LLMs, persistent multi-conversation history, and real-time message streaming for responsive interactions. All conversations are stored locally in postgres.

### Basic auth

Login with username or email and password, api returns JWT to be stored in local storage on client. Sign up also logs user in.

### OAuth

Use external providers to login (ex: google). If account already exists with base auth, we simply add the provider to the existing identity as loginInfo. If the user doesn't exist we create them without password, only accessible with the provider.

## Tech Stack

- Frontend: Angular
- Backend: ASP.NET CORE
- Database: PostgreSQL
- Hosting: NGINX, Raspberry Pi 4, Cloudflared tunnels

## Deployement

### Local

```
# appsettings.json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "EduMeilleurAPIContext": "Host=localhost;Database=EduMeilleurAPIContext;Username=postgres;Password=edumeilleur"
  },
  "OpenRouter": {
    "ApiKey": "superduperkey"
  },
  "JWT": {
    "Key": "superduperkey",
    "Issuer": "https://localhost:7027/",
    "Audience": "http://localhost:4200/"
  },
  "Admin": {
    "Email": "2ariasat@gmail.com",
    "Password": "password"
  },
  "Teacher": {
    "Password": "password"
  },
  "Teacher2": {
    "Password": "password"
  },
  "Teacher3": {
    "Password": "password"
  },
  "Teacher4": {
    "Password": "password"
  },
  "Teacher5": {
    "Password": "password"
  },
  "BREVO": {
    "API": {
      "KEY": "superduperkey"
    },
    "SENDER": {
      "EMAIL": "support@edumeilleur.ca",
      "NAME": "EduMeilleur team"
    }
  },
  "AUTH": {
    "GOOGLE": {
      "CLIENT": {
        "ID": "superduperkey",
        "SECRET": "superduperkey"
      }
    }
  }
}
```

### Prod

```
# cloudflared/config.yaml

tunnel: TUNNEL_ID
credentials-file: /etc/cloudflared/TUNNEL_ID.json

ingress:
  - hostname: domain.com
    service: http://nginx:80

  - hostname: api.domain.com
    service: http://nginx:80

  - service: http_status:404
```

```
# .env

EDUMEILLEUR_POSTGRES_DATABASE='EduMeilleurAPIContext'
EDUMEILLEUR_POSTGRES_PASSWORD='edumeilleur'
EDUMEILLEUR_POSTGRES_USER='postgres'
EDUMEILLEUR_POSTGRES_HOST='db'

JWT__Key='superduperkey'
JWT__Issuer='https://localhost:7027'
JWT__Audience='http://localhost:4200'

OpenRouter__ApiKey='superduperkey'

Admin__Email='crazy@email.com'
Admin__Password='edumeilleur'

Teacher__Password='somepassword'
Teacher2__Password='somepassword'
Teacher3__Password='somepassword'
Teacher4__Password='somepassword'
Teacher5__Password='somepassword'

BREVO__API__KEY='api-key-hurray'
BREVO__SENDER__EMAIL='support@edumeilleur.ca'
BREVO__SENDER__NAME='EduMeilleur Team'

AUTH__GOOGLE__CLIENT__ID='client-id'
AUTH__GOOGLE__CLIENT__SECRET='client-secret'
```
