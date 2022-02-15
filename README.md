# Travail Pratique #1 - Création d’un agent aspirateur
# 
## _8INF846 – Intelligence Artificielle – Hiver 2022_
#
# Membres du groupe

Ce travail a été réalisé par quatre étudiants de l'UQAC :

- CLAVIER Thomas
- CRENN Damien
- CUILLANDRE Tony
- IQUEL Fergal

## Description

Dans le cadre du cours 8INF846 - Intelligence Artificielle, nous avons développé un agent aspirateur sur Unity. L'agent doit nettoyer une maison de 25 pièces de manière autonome. Il doit alterner entre nettoyer la poussière et récupérer les bijoux au sol.

## Fonctionnalités

- Utilisation d'un algorithme d'exploration non-informée. (**Algorithme BFS - Breadth-first Search**)
- Utilisation d'un algorithme d'exploration informée. (**Algorithme A***)
- **Utilisation de capteurs et d'actionneurs** pour l'agent aspirateur.
- **Aspiration des poussières** présentes dans les pièces de la maison.
- **Collecte des bijoux** présents dans les pièces de la maison.
- **Génération sporadique d'objets** dans les pièces de la maison.
- **Modélisation BDI (Belief-Desire-Intention)** de l'agent aspirateur.
- **Nettoyage complet des 25 pièces** de la maison par l'agent aspirateur.
- **Affichage des différents paramètres** d'éxecution en temps réel.

## Lancement et Utilisation

L'application se lance grâce au fichier suivant :

```sh
Aspirobot.exe
```

Il n'y a aucune interaction à fournir, l'aspirateur va se mettre en marche dès le lancement du programme. Une interface affiche les informations liées au travail de l'agent.

## Problèmes connus

- Des fois, le robot aspire tous les déchets au moment où il passe en mode recherche informée, ce qui cause l'arret du programme. Cela est dû au coté aléatoire du spawner d'objet, il faut relancer le programme.
