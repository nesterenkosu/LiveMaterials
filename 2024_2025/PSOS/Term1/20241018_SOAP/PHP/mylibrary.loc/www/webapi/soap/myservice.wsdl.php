<?php header("Content-Type: text/xml; charset=utf-8");
echo "<?xml version=\"1.0\" encoding=\"utf-8\"?>"; ?>
<definitions xmlns:soap="http://schemas.xmlsoap.org/wsdl/soap/"
             xmlns:soapenc="http://schemas.xmlsoap.org/soap/encoding/"
             xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
             xmlns:mime="http://schemas.xmlsoap.org/wsdl/mime/"
             xmlns:tns="http://localhost/"
             xmlns:xs="http://www.w3.org/2001/XMLSchema"
             xmlns:soap12="http://schemas.xmlsoap.org/wsdl/soap12/"
             xmlns:http="http://schemas.xmlsoap.org/wsdl/http/"
             name="MyServiceWsdl"
             xmlns="http://schemas.xmlsoap.org/wsdl/"
			 targetNamespace="http://localhost/">
	<!-- Типы данных, используемые в качестве аргументов и возвращаемых значений -->
	<types>
		<xs:schema elementFormDefault="qualified"
                   xmlns:tns="http://localhost/"
                   xmlns:xs="http://www.w3.org/2001/XMLSchema"
                   targetNamespace="http://localhost/">				   
				    <xs:element name="GreetUser_Request">					
						<xs:complexType>
							<!--Объявление формата аргументов сервиса-->
							<xs:sequence>
								<xs:element name="user_name" type="xs:string" minOccurs="1" maxOccurs="1" />
							</xs:sequence>
						</xs:complexType>
					</xs:element>
					<xs:element name="GreetUser_Response">					
						<xs:complexType>
							<!--Объявление формата возвращаемого значения-->	
							<xs:sequence>
								<xs:element name="answer" type="xs:string" minOccurs="1" maxOccurs="1" />
							</xs:sequence>						
						</xs:complexType>
					</xs:element>					
		 </xs:schema>
	</types>
	<!-- Сообщения процедуры  -->
    <message name="GreetUser_RequestMessage">
        <part name="parameters" element="tns:GreetUser_Request" />
    </message>
    <message name="GreetUser_ResponseMessage">
        <part name="parameters" element="tns:GreetUser_Response" />
    </message>	
	
	 <!-- Привязка процедуры к сообщениям -->
    <portType name="MyServicePortType">
        <operation name="GreetUser">
            <input message="tns:GreetUser_RequestMessage" />
            <output message="tns:GreetUser_ResponseMessage" />
        </operation>		
    </portType>
	<!--Формат процедур веб-сервиса -->
    <binding name="MyServiceBinding" type="tns:MyServicePortType">
        <soap:binding transport="http://schemas.xmlsoap.org/soap/http" />
		<!--Объявление списка процедур-->
        <operation name="GreetUser">
            <soap:operation soapAction="" />
            <input>
                <soap:body use="literal" />
            </input>
            <output>
                <soap:body use="literal" />
            </output>
        </operation>
    </binding>
	<!--Определение сервиса -->
    <service name="MyService">
        <port name="MyServicePort" binding="tns:MyServiceBinding">
            <soap:address location="http://mylibrary.loc/webapi/soap/server.php"/>
        </port>
    </service>
</definitions>