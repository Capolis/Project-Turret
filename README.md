# Project Turret 🚀

**Project Turret** is a 2D Arcade Space Shooter developed in **Unity** using **C#**. The goal is to defend the central tower against endless swarms of enemies and asteroids while managing upgrades to survive as long as possible.

## 🎮 Gameplay Features
* **Endless Survival:** Survive against increasing numbers of enemies.
* **Upgrade System:** Real-time shop system to upgrade Fire Rate, Projectile Count (Multishot), and Pierce stats using XP.
* **Enemy Variety:** Implemented using **Inheritance** (Base Enemy, Ranged Shooter, and Physics-based Asteroids).
* **Game Feel:** Includes Screen Shake, Floating Damage Text, Particle Systems, and Slow-motion effects on level up.
* **Audio System:** robust audio manager handling SFX and Music with volume persistence.

## 🛠️ Technical Highlights
This project was created to apply core Game Development concepts:
* **Physics 2D:** Layer-based collision matrix to optimize interactions between projectiles, enemies, and the player.
* **Object-Oriented Programming (OOP):** Used Polymorphism and Inheritance for enemy behaviors (e.g., `ShooterEnemy` inherits from `EnemyController`).
* **Singleton Pattern:** Used for global managers like `AudioManager`, `GameManager`, and `LevelSystem`.
* **Coroutines:** Used for smooth visual effects and time manipulation (slow-motion).
* **Data Persistence:** Using `PlayerPrefs` to save volume settings and upgrade states during the session.

## 🕹️ Controls
* **Mouse:** Aim turret.
* **Left Click:** Shoot.
* **UI:** Click on upgrade cards when leveling up.

---
*Developed by [Caio]. Play the WebGL version on [(https://capolis.itch.io/project-turret-last-stand-protocol)].*
