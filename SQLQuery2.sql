CREATE TABLE information (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    tc_id VARCHAR(11) NOT NULL UNIQUE,
    name VARCHAR(25) NOT NULL,
    surname VARCHAR(25) NOT NULL,
    gender VARCHAR(10) NOT NULL,
    birth_date VARCHAR(10) NOT NULL,
    birth_place VARCHAR(50) NOT NULL,
    mother_name VARCHAR(20) NOT NULL,
    father_name VARCHAR(20) NOT NULL
);

INSERT INTO information (tc_id, name, surname, gender, birth_date, birth_place, mother_name, father_name)
VALUES ('75554367891', 'Başak', 'Altunterim', 'Kadın', '2002-03-09', 'Kocaeli', 'Arzu', 'Muhammet');
