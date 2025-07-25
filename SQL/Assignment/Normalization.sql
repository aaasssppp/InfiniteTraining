-- create table clientrental
CREATE TABLE clientrental (
clientno VARCHAR(10),
cname VARCHAR(100),
propertyno VARCHAR(10),
paddress VARCHAR(200),
rentstart DATE,
rentfinish DATE,
rent DECIMAL(10, 2),
ownerno VARCHAR(10),
oname VARCHAR(100))
 
 -- insert values to table clientrental
INSERT INTO clientrental (clientno, cname, propertyno, paddress, rentstart, rentfinish, rent, ownerno, oname) VALUES
('cr76', 'john kay', 'pg4', '6 lawrence st, glasgow', '2001-10-01', '2001-08-31', 350, 'co40', 'tina murphy'),
('cr76', 'john kay', 'pg16', '5 novar dr, glasgow', '2002-09-01', '2002-09-01', 450, 'co93', 'tony shaw'),
('cr76', 'john kay', 'pg4', '6 lawrence st, glasgow', '1999-09-01', '2000-06-10', 350, 'co40', 'tina murphy'),
('cr56', 'aline stewart', 'pg36', '2 manor rd, glasgow', '2000-10-10', '2001-12-01', 370, 'co93', 'tony shaw'),
('cr56', 'aline stewart', 'pg16', '5 novar dr, glasgow', '2002-11-01', '2003-08-01', 450, 'co93', 'tony shaw')

-- create table client
CREATE TABLE client (
clientno VARCHAR(10) PRIMARY KEY,
cname VARCHAR(100))
 
 -- inssert values to table client
INSERT INTO client (clientno, cname) VALUES
('cr76', 'john kay'),
('cr56', 'aline stewart')
 
-- create table propertydetails
CREATE TABLE propertydetails (
propertyno VARCHAR(10) PRIMARY KEY,
paddress VARCHAR(200),
rent DECIMAL(10,2),
ownerno VARCHAR(10))
 
 -- insert values into propertydetails
INSERT INTO propertydetails (propertyno, paddress, rent, ownerno) VALUES
('pg4', '6 lawrence st, glasgow', 350, 'co40'),
('pg16', '5 novar dr, glasgow', 450, 'co93'),
('pg36', '2 manor rd, glasgow', 370, 'co93')
 
-- create table clientrental
CREATE TABLE clientrental2ndNF (
clientno VARCHAR(10),
propertyno VARCHAR(10),
rentstart DATE,
rentfinish DATE,
PRIMARY KEY (clientno, propertyno, rentstart),
FOREIGN KEY (clientno) REFERENCES client(clientno),
FOREIGN KEY (propertyno) REFERENCES propertydetails(propertyno))
 
 -- insert values in clientrental2ndNF
INSERT INTO clientrental2ndNF (clientno, propertyno, rentstart, rentfinish) VALUES
('cr76', 'pg4', '2001-10-01', '2001-08-31'),
('cr76', 'pg16', '2002-09-01', '2002-09-01'),
('cr76', 'pg4', '1999-09-01', '2000-06-10'),
('cr56', 'pg36', '2000-10-10', '2001-12-01'),
('cr56', 'pg16', '2002-11-01', '2003-08-01')
 
-- create table owner
CREATE TABLE OWNER (
ownerno VARCHAR(10) PRIMARY KEY,
oname VARCHAR(100))
 
 -- insert values into owner table
INSERT INTO OWNER (ownerno, oname) VALUES
('co40', 'tina murphy'),
('co93', 'tony shaw')
 
-- propertydetails table is updated with foreign key
CREATE TABLE propertydetails3rdNF(
propertyno VARCHAR(10) PRIMARY KEY,
paddress VARCHAR(200),
rent DECIMAL(10,2),
ownerno VARCHAR(10),
FOREIGN KEY (ownerno) REFERENCES OWNER(ownerno))
 
 -- insert values into propertydetails3nf
INSERT INTO propertydetails3rdNF VALUES
('pg4', '6 lawrence st, glasgow', 350, 'co40'),
('pg16', '5 novar dr, glasgow', 450, 'co93'),
('pg36', '2 manor rd, glasgow', 370, 'co93')
