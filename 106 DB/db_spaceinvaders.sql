DROP DATABASE IF EXISTS db_spaceinvaders;

-- Create the database
CREATE DATABASE db_spaceinvaders;

-- Use the created database
USE db_spaceinvaders;

-- Drop existing tables if they exist
DROP TABLE IF EXISTS jouer_dans;
DROP TABLE IF EXISTS etre_dans;
DROP TABLE IF EXISTS faire_partie_de;
DROP TABLE IF EXISTS Score;
DROP TABLE IF EXISTS Player;
DROP TABLE IF EXISTS Enemy;
DROP TABLE IF EXISTS Level;  
DROP TABLE IF EXISTS Obstacle;  

-- Create Level table
CREATE TABLE Level (
    level_id INT AUTO_INCREMENT PRIMARY KEY, 
    levelName VARCHAR(50) 
);

-- Create Obstacle table
CREATE TABLE Obstacle (
    obstacle_id INT AUTO_INCREMENT PRIMARY KEY,  
    obstacleType VARCHAR(50) 
);

-- Create Enemy table
CREATE TABLE Enemy (
    enemy_id INT AUTO_INCREMENT PRIMARY KEY, 
    baseHP INT,
    sprite VARCHAR(50),
    posX INT,
    posY INT,
    currentHP INT
);

-- Create Player table
CREATE TABLE Player (
    player_id INT AUTO_INCREMENT PRIMARY KEY,  
    playerName VARCHAR(50),
    sprite VARCHAR(50),
    posX INT,
    currentLives INT,
    startingHealth INT
);

-- Create Score table
CREATE TABLE Score (
    score_id INT AUTO_INCREMENT PRIMARY KEY,  
    creationTime DATETIME DEFAULT CURRENT_TIMESTAMP, 
    score INT,
    level_fk INT NOT NULL,  
    player_fk INT NOT NULL,  
    FOREIGN KEY (level_fk) REFERENCES Level(level_id),
    FOREIGN KEY (player_fk) REFERENCES Player(player_id)
);

-- Create faire_partie_de table
CREATE TABLE faire_partie_de (
    level_fk INT, 
    enemy_fk INT,  
    PRIMARY KEY (level_fk, enemy_fk),
    FOREIGN KEY (level_fk) REFERENCES Level(level_id),
    FOREIGN KEY (enemy_fk) REFERENCES Enemy(enemy_id)
);

-- Create etre_dans table
CREATE TABLE etre_dans (
    obstacle_fk INT,  
    level_fk INT,  
    PRIMARY KEY (obstacle_fk, level_fk),
    FOREIGN KEY (obstacle_fk) REFERENCES Obstacle(obstacle_id),
    FOREIGN KEY (level_fk) REFERENCES Level(level_id)
);

-- Create jouer_dans table
CREATE TABLE jouer_dans (
    level_fk INT,  
    player_fk INT, 
    PRIMARY KEY (level_fk, player_fk),
    FOREIGN KEY (level_fk) REFERENCES Level(level_id),
    FOREIGN KEY (player_fk) REFERENCES Player(player_id)
);




-- Obstacle Data
INSERT INTO Obstacle (obstacle_id, obstacleType) VALUES 
(1, 'lvl1_obstacle_sprite.png'),
(2, 'lvl1_obstacle_sprite.png'),
(3, 'lvl1_obstacle_sprite.png'),
(4, 'lvl1_obstacle_sprite.png'),
(5, 'lvl1_obstacle_sprite.png'),
(6, 'lvl2_obstacle_sprite.png'),
(7, 'lvl2_obstacle_sprite.png'),
(8, 'lvl2_obstacle_sprite.png'),
(9, 'lvl2_obstacle_sprite.png'),
(10, 'lvl2_obstacle_sprite.png');

-- Level Data
INSERT INTO Level (level_id, levelName) VALUES 
(1, 'Space'),
(2, 'Forest'),
(3, 'Castle'),
(4, 'Dungeon'),
(5, 'Mountain'),
(6, 'Ravine'),
(7, 'Hell'),
(8, 'Cave'),
(9, 'Graveyard'),
(10, 'Volcano');

-- Enemy Data
INSERT INTO Enemy (enemy_id, baseHP, sprite, posX, posY, currentHP) VALUES 
(1, 3, 'lvl1_redInvader_sprite.png', 120, 200, 1),
(2, 5, 'lvl1_blueInvader_sprite.png', 300, 450, 5),
(3, 2, 'lvl1_greenInvader_sprite.png', 50, 100, 1),
(4, 6, 'lvl1_yellowInvader_sprite.png', 400, 500, 5),
(5, 2, 'lvl1_purpleInvader_sprite.png', 200, 300, 2),
(6, 8, 'lvl2_redInvader_sprite.png', 600, 700, 7),
(7, 4, 'lvl2_blueInvader_sprite.png', 100, 150, 3),
(8, 4, 'lvl2_greenInvader_sprite.png', 500, 550, 4),
(9, 3, 'lvl2_yellowInvader_sprite.png', 200, 250, 3),
(10, 7, 'lvl2_purpleInvader_sprite.png', 650, 700, 6);

-- Player Data
INSERT INTO Player (player_id, playerName, sprite, posX, currentLives, startingHealth) VALUES 
(1, 'Rom1', 'ship_sprite.png', 100, 3, 5),
(2, 'Rom2', 'ship_sprite.png', 150, 1, 5),
(3, 'Fabrice', 'ship_sprite.png', 50, 3, 5),
(4, 'MeowMan', 'ship_sprite.png', 200, 4, 5),
(5, 'Rom3', 'ship_sprite.png', 180, 5, 4),
(6, 'Trestrestreslongnom', 'ship_sprite.png', 130, 5, 5),
(7, 'CH', 'ship_sprite.png', 220, 5, 5),
(8, 'Hecarim', 'ship_sprite.png', 170, 5, 5),
(9, 'Graves', 'ship_sprite.png', 210, 5, 5),
(10, 'Tahliyah', 'ship_sprite.png', 160, 5, 5);

-- Score Data
INSERT INTO Score (score_id, creationTime, score, level_fk, player_fk) VALUES 
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

-- faire_partie_de Data
INSERT INTO faire_partie_de (level_fk, enemy_fk) VALUES 
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

-- etre_dans Data
INSERT INTO etre_dans (obstacle_fk, level_fk) VALUES 
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

-- jouer_dans Data
INSERT INTO jouer_dans (level_fk, player_fk) VALUES 
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

