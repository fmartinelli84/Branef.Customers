FROM mongodb/mongodb-community-server

ARG MONGODB_INITDB_ROOT_USERNAME
ARG MONGODB_INITDB_ROOT_PASSWORD

HEALTHCHECK --interval=10s --timeout=3s --start-period=10s --retries=10 \
  CMD echo 'db.runCommand("ping").ok' | mongosh mongodb://$MONGODB_INITDB_ROOT_USERNAME:$MONGODB_INITDB_ROOT_PASSWORD@localhost:27017/?authSource=admin --quiet