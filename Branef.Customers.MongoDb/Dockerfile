FROM mongodb/mongodb-community-server

#ARG MONGODB_INITDB_ROOT_USERNAME
#ARG MONGODB_INITDB_ROOT_PASSWORD

HEALTHCHECK --interval=10s --timeout=20s --start-period=10s --retries=10 \
  CMD echo 'db.runCommand("ping").ok' | mongosh mongodb://localhost:27017 --quiet
#  CMD echo 'db.runCommand("ping").ok' | mongosh mongodb://$MONGODB_INITDB_ROOT_USERNAME:$MONGODB_INITDB_ROOT_PASSWORD@localhost:27017/?connectTimeoutMS=60000&socketTimeoutMS=60000&waitQueueTimeoutMS=60000&authSource=admin&readPreference=primary&appname=Customers&ssl=false --quiet
  