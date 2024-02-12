# Logstash Run Command For Docker and Mounts
docker run -d --name logstash --network=host -v C:/Users/Bariscan/Downloads/logstash-8.12.0/logstash-core/lib/jars:/usr/share/logstash/logstash-core/lib/jars -v C:/Users/Bariscan/Downloads/logstash-8.12.0/conf:/usr/share/logstash/config docker.elastic.co/logstash/logstash:8.12.0

# Conf for Postgres - postgre.conf
input {
  jdbc {
    jdbc_connection_string => "jdbc:postgresql://host.docker.internal:5432/postgres" 
    jdbc_user => "user"
    jdbc_password => "admin"
    schedule => "*/1 * * * *"
    jdbc_driver_class => "org.postgresql.Driver"
    jdbc_driver_library => "/usr/share/logstash/logstash-core/lib/jars/postgresql-jdbc.jar"
    statement => "SELECT * from public.users"
  }
}

output {
  elasticsearch {
    hosts => ["https://0.0.0.0:9200"]
    index => "test_index"
    document_id => "users_%{uuid}"
    #doc_as_upsert => true
    user => "elastic"
    password => "IBM=5L-kCO79sYb28NLV"
    ssl_verification_mode => none
    ssl_enabled => true
  }
}

# Conf for Pipeline - logstash.yml
pipeline.id: users-pipeline
path.config: "/usr/share/logstash/config/postgre.conf"
