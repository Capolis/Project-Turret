<h1>Nome do Projeto: Project Turret (Last Stand Protocol)</h1>
<h1>Gênero: Stationary Tower Defense / Bullet Heaven</h1>
<h1>Engine: Unity 2022/2023 (2D Core)</h1>
<h1>Status: Protótipo Funcional (Core Loop)</h1>

<p><h2>1. Mecânicas Implementadas</h2></p>
<ul><li>Stationary Player: O jogador é uma torre fixa em (0,0). Rotaciona seguindo o cursor do mouse (Input System: Both).</li>

<li>Auto-Fire: Disparo automático de projéteis com Cooldown fixo.</li>

<li>Horde Spawner: Inimigos instanciados em anel ao redor da câmera, movendo-se linearmente para o centro.</li>

<li>Combat System:</li>

<ul><li>Player Hit: Inimigo (Trigger) toca no Player -> Causa Dano -> Inimigo morre.</li>

<li>Enemy Hit: Projétil (Trigger) toca no Inimigo -> Inimigo morre -> Projétil destrói.</li>

Progression Loop: Inimigos dropam XP (Gems) ao morrerem por tiro. Gems são magnéticas e voam para o player. Acúmulo de XP gera Level Up.</li> </ul>
</ul>

<p><h2>2. Arquitetura de Scripts (C#)</h2></p>
<ul><li>TurretController.cs: Controla rotação (Mathf.Atan2) e instância de balas (Timer).</li>

<li>Projectile.cs: Movimento linear (Translate.up) e autodestruição por tempo.</li>

<li>EnemyController.cs: Movimento (MoveTowards), detecção de colisão (OnTriggerEnter2D) e drop de Loot.</li>

<li>EnemySpawner.cs: Instancia inimigos em posições aleatórias (Random.insideUnitCircle) fora da tela.</li>

<li>PlayerHealth.cs: Gerencia HP e reinicia a cena em caso de morte (SceneManager).</li>

<li>LevelSystem.cs: Gerencia XP acumulado e dispara evento de Level Up.</li>

<li>ExperienceGem.cs: Comportamento de atração magnética para o jogador.</li>
</ul>