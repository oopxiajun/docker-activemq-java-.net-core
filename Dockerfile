FROM  webcenter/activemq:latest
COPY activemq.xml /opt/activemq/conf/activemq.xml
COPY jetty.xml /opt/activemq/conf/jetty.xml
COPY users.properties /opt/activemq/conf/users.properties
COPY run.sh /opt/activemq/bin/run.sh
RUN chmod 777 /opt/activemq/bin/run.sh
# Expose all port 暴露所有用到的端口
EXPOSE 8161
EXPOSE 61616
EXPOSE 5672
EXPOSE 61613
EXPOSE 1883
EXPOSE 61614
# 初始化 
WORKDIR  /opt/activemq/bin/
RUN  ./run.sh 