# FROM node:latest
# WORKDIR /usr/local/app
# COPY Branef.Customers.Web/package.json .
# RUN npm install --force --global @angular/cli@15.0.3
# RUN npm install --force
# COPY Branef.Customers.Web/ .
# EXPOSE 4200
# CMD [ "npm", "run", "start" ]


FROM node:18
# Setting working directory. All the path will be relative to WORKDIR
WORKDIR /usr/src/app
# Installing dependencies
COPY Branef.Customers.Web/package*.json ./
RUN npm install --force
# Copying source files
COPY Branef.Customers.Web/ .
# Building app
RUN npm run build
EXPOSE 4200
# Running the app
ENTRYPOINT ["npm", "start"]
