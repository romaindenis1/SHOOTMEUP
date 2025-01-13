DROP DATABASE IF EXISTS db_spaceinvaders;

-- Create the database
CREATE DATABASE db_spaceinvaders;

-- Use the created database
USE db_spaceinvaders;

-- Drop existing tables if they exist
DROP TABLE IF EXISTS t_jouer_dans;
DROP TABLE IF EXISTS t_etre_dans;
DROP TABLE IF EXISTS t_faire_partie_de;
DROP TABLE IF EXISTS t_score;
DROP TABLE IF EXISTS t_joueur;
DROP TABLE IF EXISTS t_ennemi;
DROP TABLE IF EXISTS t_niveau;  
DROP TABLE IF EXISTS t_obstacle;  

-- Create Level table
-- Create Level table
-- Create Level table
CREATE TABLE t_niveau (
    niveau_fk INT AUTO_INCREMENT PRIMARY KEY, 
    nom VARCHAR(50)
);

-- Create Obstacle table
CREATE TABLE t_obstacle (
    obstacle_fk INT AUTO_INCREMENT PRIMARY KEY,  
    sprite VARCHAR(50),
    posX INT,
    vieActuelle INT,
    vieDepart INT
);

-- Create Enemy table
CREATE TABLE t_ennemi (
    ennemi_fk INT AUTO_INCREMENT PRIMARY KEY, 
    vieDepart INT,
    vieActuelle INT,
    sprite VARCHAR(50),
    posX INT,
    posY INT
);

-- Create Player table
CREATE TABLE t_joueur (
    joueur_fk INT AUTO_INCREMENT PRIMARY KEY,  
    nom VARCHAR(50),
    sprite VARCHAR(50),
    posX INT,
    vieActuelle INT,
    vieDepart INT
);

-- Create Score table
CREATE TABLE t_score (
    score_fk INT AUTO_INCREMENT PRIMARY KEY,  
    dateCreation DATETIME DEFAULT CURRENT_TIMESTAMP, 
    score INT,
    niveau_fk INT NOT NULL,  
    joueur_fk INT NOT NULL,  
    FOREIGN KEY (niveau_fk) REFERENCES t_niveau(niveau_fk),
    FOREIGN KEY (joueur_fk) REFERENCES t_joueur(joueur_fk)
);

-- Create faire_partie_de table
CREATE TABLE t_faire_partie_de (
    niveau_fk INT, 
    ennemi_fk INT,  
    PRIMARY KEY (niveau_fk, ennemi_fk),
    FOREIGN KEY (niveau_fk) REFERENCES t_niveau(niveau_fk),
    FOREIGN KEY (ennemi_fk) REFERENCES t_ennemi(ennemi_fk)
);

-- Create etre_dans table
CREATE TABLE t_etre_dans (
    obstacle_fk INT,  
    niveau_fk INT,  
    PRIMARY KEY (obstacle_fk, niveau_fk),
    FOREIGN KEY (obstacle_fk) REFERENCES t_obstacle(obstacle_fk),
    FOREIGN KEY (niveau_fk) REFERENCES t_niveau(niveau_fk)
);

-- Create jouer_dans table
CREATE TABLE t_jouer_dans (
    niveau_fk INT,  
    joueur_fk INT, 
    PRIMARY KEY (niveau_fk, joueur_fk),
    FOREIGN KEY (niveau_fk) REFERENCES t_niveau(niveau_fk),
    FOREIGN KEY (joueur_fk) REFERENCES t_joueur(joueur_fk)
);

-- Obstacle
INSERT INTO t_obstacle (obstacle_fk, sprite, posX, vieDepart, vieActuelle) VALUES 
(1, 'rock.png', 15, 100, 85),
(2, 'tree.png', 25, 150, 140),
(3, 'bush.png', 35, 50, 30),
(4, 'wall.png', 45, 200, 180),
(5, 'barrel.png', 55, 80, 70),
(6, 'crate.png', 65, 90, 60),
(7, 'rock.png', 75, 120, 110),
(8, 'fence.png', 85, 110, 100),
(9, 'stone.png', 95, 130, 120),
(10, 'log.png', 105, 70, 50);

-- Level
INSERT INTO t_niveau (niveau_fk, nom) VALUES 
(1, 'Espace'),
(2, 'Foret'),
(3, 'Chateau'),
(4, 'Oubliettes'),
(5, 'Montagne'),
(6, 'Falaise'),
(7, 'Enfer'),
(8, 'Grotte'),
(9, 'Cimetiere'),
(10, 'Volcan');

-- Enemy
INSERT INTO t_ennemi (ennemi_fk, vieDepart, vieActuelle, sprite, posX, posY) VALUES
(1, 100, 80, 'ennemi_rouge.png', 120, 200),
(2, 150, 140, 'ennemi_bleu.png', 300, 450),
(3, 50, 30, 'ennemi_jaune.png', 50, 100),
(4, 200, 180, 'ennemi_vert.png', 400, 500),
(5, 80, 60, 'ennemi_violet.png', 200, 300),
(6, 120, 100, 'ennemi_rouge.png', 600, 700),
(7, 110, 90, 'ennemi_bleu.png', 100, 150),
(8, 130, 110, 'ennemi_vert.png', 500, 550),
(9, 140, 120, 'ennemi_jaune.png', 200, 250),
(10, 70, 50, 'ennemi_violet.png', 650, 700);

-- Player
INSERT INTO t_joueur (joueur_fk, nom, sprite, posX, vieActuelle, vieDepart) VALUES 
(1, 'Steven Stevenson', 'ship_sprite.png', 100, 3, 5),
(2, 'Maya Cadela', 'ship_sprite.png', 150, 1, 5),
(3, 'Fabrice', 'ship_sprite.png', 50, 3, 5),
(4, 'MeowMan', 'ship_sprite.png', 200, 4, 5),
(5, 'Rom3', 'ship_sprite.png', 180, 5, 4),
(6, 'Trestrestreslongnom', 'ship_sprite.png', 130, 5, 5),
(7, 'CH', 'ship_sprite.png', 220, 5, 5),
(8, 'Hecarim', 'ship_sprite.png', 170, 5, 5),
(9, 'Graves', 'ship_sprite.png', 210, 5, 5),
(10, 'Tahliyah', 'ship_sprite.png', 160, 5, 5);

-- Score
INSERT INTO t_score (score_fk, dateCreation, score, niveau_fk, joueur_fk) VALUES 
(1, '2024-09-15 12:12:12', 1000, 1, 1),
(2, '2024-09-15 10:45:30', 850, 2, 2),
(3, '2024-09-17 02:12:59', 950, 3, 3),
(4, '2024-09-18 23:02:13', 1100, 4, 4),
(5, '2024-09-19 10:12:23', 900, 5, 5),
(6, '2024-09-20 11:12:06', 1050, 6, 6),
(7, '2024-09-21 11:12:06', 980, 7, 7),
(8, '2024-09-22 18:08:56', 1020, 8, 8),
(9, '2024-09-23 20:12:00', 970, 9, 9),
(10, '2024-09-24 01:31:31', 1110, 10, 10);

-- faire_partie_de
INSERT INTO t_faire_partie_de (niveau_fk, ennemi_fk) VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

-- etre_dans
INSERT INTO t_etre_dans (obstacle_fk, niveau_fk) VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

-- jouer_dans
INSERT INTO t_jouer_dans  (niveau_fk, joueur_fk) VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);