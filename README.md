# ModsenMeetupAPI
 Test task for modsen internship:CRUD Web API for working with events (creation, modification, deletion,
receipt)
# Technologies used:
1. .Net 7;
2. Entity Framework Core;
3. PostgreSQL;
4. AutoMapper;
5. JWT Bearer;
6. Swagger;
7. MediatR
8. RabbitMQ
9. Docker
10. Tye
# How to run project:
1. Start DB:
   docker compose up -d
2. Start all projects:
   tye run
# Project architecture:
1. Meetup.info - Whose service stores events and some information about speakers, communicates with the speaker service using RabbitMQ 
2. Meetup.SpeakerService -A service that is responsible for speakers and communicates with the event service using RabbitMQ
3. Meetup - the service that is responsible for authentication and authorization, and is also a GateWayAPI
