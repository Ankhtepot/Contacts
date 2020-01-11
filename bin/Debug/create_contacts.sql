DROP TABLE contacts;

CREATE TABLE contacts (
        ID number primary key,
        NAME VARCHAR(50),
        EMAIL VARCHAR(50),
        PHONE VARCHAR(50)
    );
    
DROP SEQUENCE CONTACTS_ID_SEQ;
CREATE SEQUENCE CONTACTS_ID_SEQ
    INCREMENT BY 1
    START WITH 1
    NOCACHE
    NOCYCLE;
    
INSERT INTO contacts VALUES(contacts_id_seq.nextval, 'testName', 'testEmail', 'testPhone');
        