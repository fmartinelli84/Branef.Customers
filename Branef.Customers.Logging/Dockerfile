FROM datalust/seq

RUN apt-get update 
RUN apt-get --yes install curl

HEALTHCHECK --interval=5s CMD curl "http://localhost/api" --fail --show-error --silent