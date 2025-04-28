DROP DATABASE IF EXISTS Dybss;

CREATE DATABASE Dybss;

USE Dybss;

SET @cle='DybVicQuebec';

CREATE TABLE users(
	id INT PRIMARY KEY AUTO_INCREMENT,
    nom VARCHAR(255),
    prenom VARCHAR(255),
    email VARCHAR(255),
    motDePasse BLOB,
    role_user enum('Admin', 'client', 'Employe') DEFAULT 'Client',
    CONSTRAINT email_check CHECK(email RLIKE '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$')
);

CREATE TABLE produits(
	id INT PRIMARY KEY AUTO_INCREMENT,
    nom VARCHAR(255),
    code VARCHAR(255),
    categorie enum('Dessert','Boisson', 'Sandwich') DEFAULT 'Boisson'
);

Alter table users modify column email VARCHAR(255) unique;

CREATE TABLE employes(
	code VARCHAR(7) PRIMARY KEY,
    id_utilisateur INt,
    FOREIGN KEY (id_utilisateur) REFERENCES users(id)
);
