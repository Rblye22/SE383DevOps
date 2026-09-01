FROM mysql:8.0

ENV MYSQL_DATABASE=defaultdb

COPY initialization-scripts/ /docker-entrypoint-initdb.d/

EXPOSE 3306