using UnityEngine;

public class TurretController : MonoBehaviour{

    [Header("Configuração Base (Valores Originais)")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    // BASE: Estes valores nunca mudam durante o jogo. São o ponto de partida.
    public float baseFireRate = 0.5f;
    public int baseProjectileCount = 1;

    [Header("Configurações do Leque")]
    public float spreadAngle = 20f;

    // ATUAIS: Estas variáveis são calculadas (Base + Nível da Loja)
    // Usamos [HideInInspector] para você não mexer nelas sem querer na Unity
    [HideInInspector] public float currentFireRate;
    [HideInInspector] public int currentProjectileCount;
    private int currentPierceLevel = 0;

    private float nextFireTime = 0f;

    // Chaves de salvamento
    const string PIERCE_LVL_KEY = "Shop_PierceLvl";
    const string FIRERATE_LVL_KEY = "Shop_FireRateLvl";
    const string PROJECTILE_LVL_KEY = "Shop_ProjectileLvl";

    void Start(){
        // Ao iniciar, calcula os status baseados no que está salvo
        UpdateTurretStats();
    }

    // --- ESSA É A FUNÇÃO QUE A LOJA VAI CHAMAR ---
    public void UpdateTurretStats(){
        // 1. Carrega os níveis salvos
        int fireRateLevel = PlayerPrefs.GetInt(FIRERATE_LVL_KEY, 0);
        int projectileLevel = PlayerPrefs.GetInt(PROJECTILE_LVL_KEY, 0);
        currentPierceLevel = PlayerPrefs.GetInt(PIERCE_LVL_KEY, 0);

        // 2. Cálculo Seguro do Fire Rate
        // Pega o BASE (0.5) e multiplica pela potência.
        // Nunca modificamos a variável 'baseFireRate'
        currentFireRate = baseFireRate * Mathf.Pow(0.9f, fireRateLevel);

        // Trava de segurança: Limite de velocidade (0.05s)
        if (currentFireRate < 0.05f) currentFireRate = 0.05f;

        // 3. Cálculo Seguro dos Projéteis
        // Base (1) + Nível comprado. Simples e sem erro.
        currentProjectileCount = baseProjectileCount + projectileLevel;
    }

    void Update(){
        if (Time.timeScale == 0) return;

        RotateTowardsMouse();
        HandleShooting();
    }

    void RotateTowardsMouse(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void HandleShooting(){
        // Usa a variável calculada 'currentFireRate'
        if (Time.time >= nextFireTime){
            Shoot();
            nextFireTime = Time.time + currentFireRate;
        }
    }

    void Shoot(){
        if (AudioManager.instance != null)
            AudioManager.instance.PlayShoot();

        // Se for tiro único
        if (currentProjectileCount == 1){
            CreateBullet(transform.rotation);
        }
        else{
            // Lógica do Leque (Shotgun)
            float startRotation = -spreadAngle / 2f;
            float angleStep = spreadAngle / (currentProjectileCount - 1);

            for (int i = 0; i < currentProjectileCount; i++)
            {
                float currentAngle = startRotation + (angleStep * i);
                // Soma a rotação da torre com a rotação do leque
                Quaternion rotation = transform.rotation * Quaternion.Euler(0, 0, currentAngle);

                CreateBullet(rotation);
            }
        }
    }

    // Função auxiliar para criar a bala e configurar o Pierce
    // Evita repetir código e evita o erro de instanciar 2x
    void CreateBullet(Quaternion rotation){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);

        Projectile bulletScript = bullet.GetComponent<Projectile>();

        if (bulletScript == null)
            bulletScript = bullet.GetComponent<Projectile>(); // Tenta achar com o outro nome caso tenha mudado

        if (bulletScript != null){
            bulletScript.pierceCount = 1 + currentPierceLevel;
        }
    }

}