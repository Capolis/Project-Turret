<h1>Nome do Projeto: Project Turret (Last Stand Protocol) GDD</h1>
<h1>Versão: 1.0 (MVP Completed)</h1>
<h1>Gênero: Stationary Tower Defense / Bullet Heaven</h1>
<h1>Engine: Unity 2022/2023 (2D Core)</h1>
<h1>Status: v 1.0 Funcional (Core Loop)</h1>

<p><h2>1. Visão Geral (High Concept)</h2></p>
<ul><li>Um jogo de sobrevivência arcade onde o jogador controla uma torre de defesa estática no centro do universo. Incapaz de fugir, o jogador deve girar, atirar e evoluir para resistir a ondas infinitas de satélites hostis e lixo espacial senciente.</li>

<ul><li>Hook: "Você não se move. Você só atira e evolui."</li></ul>
</ul>

<p><h2>2. Mecânicas Principais (Core Mechanics)</h2></p>
<ul><h3>2.1. O Jogador (The Turret)</h3>
<li>Movimento: Nulo (Posição fixa em 0,0).</li>

<li>Mira: Rotação 360º seguindo o cursor do mouse (Input System Híbrido).</li>

<li>Disparo: Automático com cadência definida (Auto-fire).</li>

<li>ida: Sistema de HP finito. Game Over retorna ao Menu Principal.</li>

<h3>2.2. Inimigos (The Horde)</h3>
<li>Spawn: Procedural, em anel ao redor da câmera (fora da visão).</li>

<li>Comportamento: Perseguição linear em direção ao centro (MoveTowards).</li>

<li>Dano: Colisão física (Kamikaze). Causa dano ao tocar na torre e se autodestrói.</li>

<li>Visual: Rotação própria (Spinning) para dinamismo visual.</li>

<li>Feedback: Hit Flash (piscar branco/vermelho) ao receber dano.</li>

<h3>2.3. Sistema de Progressão (RPG Elements)</h3>
<li>XP: Inimigos dropam "Gemas de XP" ao morrer por projéteis.</li>

<li>Magnetismo: Gemas voam automaticamente para o jogador.</li>

<li>Level Up: Ao preencher a barra de XP, o jogo pausa (TimeScale 0).</li>

<li>Upgrades: Menu de seleção RNG com 3 opções (atualmente implementadas):</li>

<ul><li>Fire Rate ++: Aumenta velocidade de disparo em 10%.</li>

<li>Heal: Recupera 2 HP.</li>

<li>Multishot: Adiciona +1 projétil em leque (Shotgun effect).</li>
</ul>
</ul>

<h3>3. Personagens e InimigosTipoNomeHPSpeedXP</h3>
<ul>
<table>
  <thead>
    <tr>
      <th>Tipo</th>
      <th>Nome</th>
      <th>HP</th>
      <th>Speed</th>
      <th>XP Reward</th>
      <th>Comportamento</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><strong>Player</strong></td>
      <td>Turret Alpha</td>
      <td>5</td>
      <td>N/A</td>
      <td>N/A</td>
      <td>Controlado pelo jogador.</td>
    </tr>
    <tr>
      <td><strong>Inimigo 1</strong></td>
      <td>Satélite Scout</td>
      <td>1</td>
      <td>3.0</td>
      <td>10</td>
      <td>Básico, morre com 1 hit.</td>
    </tr>
    <tr>
      <td><strong>Inimigo 2</strong></td>
      <td>Heavy Tank</td>
      <td>2</td>
      <td>2.5</td>
      <td>30</td>
      <td>Lento, exige 2 hits. Cor Avermelhada.</td>
    </tr>
    <tr>
      <td><strong>Inimigo 3</strong></td>
      <td>Interceptor</td>
      <td>1</td>
      <td>5.0</td>
      <td>20</td>
      <td>Muito rápido, difícil de mirar.</td>
    </tr>
    <tr>
      <td><strong>Inimigo 4</strong></td>
      <td>Mothership (Boss)</td>
      <td>10</td>
      <td>1.5</td>
      <td>200</td>
      <td>Gigante, esponja de balas.</td>
    </tr>
  </tbody>
</table>
<li>Nota: O aparecimento dos inimigos é controlado por nível (Ex: Tank só aparece no Lvl 2+).</li>
</ul>

<h3>4. Interface (UI/UX)</h3>
<ul>
<li>HUD (Heads-Up Display):

<li>Barra de Vida (Vermelha, Fill Amount).

<li>Barra de XP (Amarela/Azul, começa vazia).

<li>Indicador de Nível Atual.

<li>Score Atual.

<li>Menus:

<li>Main Menu: Título, High Score (persistente via PlayerPrefs), Botão Start.

<li>Level Up Screen: Painel Overlay (Pausa o jogo), Botões interativos.

<li>Cenário:

<ul><li>Sistema de Fundos Aleatórios: Carrega uma imagem espacial diferente a cada partida.</li></ul>
</ul>


<h3>5. Áudio</h3>
<ul>
<li>SFX (Efeitos):</li>

<ul><li>Shoot.wav: Disparo laser curto.</li>

<li>Explosion.wav: Som grave na morte do inimigo.</li>

<li>LevelUp.wav: Feedback positivo na evolução.</li></ul>

<li>Arquitetura: AudioManager (Singleton) para garantir sons contínuos mesmo após destruição de objetos.</li>
</ul>

<h3>6. Arquitetura Técnica (Scripts)</h3>
<ul>
<li>Game Loop: GameManager (Gerencia estados).</li>

<li>Persistência: ScoreManager salva Recorde no registro do OS.</li>

<li>Spawner: EnemySpawner utiliza lista de Structs para configurar ondas baseadas em nível.</li>

<li>Versão: Controle via Git/GitHub (Assets pesados filtrados via .gitignore).</li>
</ul>