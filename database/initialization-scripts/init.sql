CREATE DATABASE IF NOT EXISTS defaultdb;
USE defaultdb;

CREATE TABLE IF NOT EXISTS Questions (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    OptionA VARCHAR(255) NOT NULL,
    OptionB VARCHAR(255) NOT NULL,
    VotesA INT NOT NULL DEFAULT 0,
    VotesB INT NOT NULL DEFAULT 0
);

INSERT INTO Questions (OptionA, OptionB, VotesA, VotesB) VALUES
('Have the ability to fly', 'Have the ability to become invisible', 1, 4),
('Fight one horse-sized duck', 'Fight 100 duck-sized horses', 5, 0),
('Live without music', 'Live without movies', 0, 8),
('Know when you will die', 'Know how you will die', 1, 10),
('Be able to teleport anywhere', 'Be able to read minds', 5, 2),
('Have super strength', 'Have super speed', 3, 2),
('Explore outer space', 'Explore the deep ocean', 13, 0);