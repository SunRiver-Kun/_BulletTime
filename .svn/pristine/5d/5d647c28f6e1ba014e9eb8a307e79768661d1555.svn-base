using UnityEngine;
using UnityEngine.Animations;
using Utility;

[RequireComponent(typeof(BlackAnim))]
[RequireComponent(typeof(Rigidbody2D))]
public class Black : MonoBehaviour
{
    [Header("基本速度")]
    [Min(0f),Tooltip("玩家移动速度")]
    public float moveSpeed = 15;
    [Min(0f),Tooltip("跳跃速度,不受刚体的重力比例影响")]
    public float jumpSpeed = 20;
    [Min(0f),Tooltip("被秒针撞到时给予的速度")]
    public float clockSpeed = 20;
    [Min(0),Tooltip("跳跃次数")]
    public int maxJumpCount=1;
    public int currentJumpCount=1;
    [Min(0f),Tooltip("上跳重力倍率")]
    public float m_gravitySacleUp=5;
    [Min(0f),Tooltip("下落重力倍率")]
    public float m_gravitySacleDown=9;
    [Header("冲刺")]
    [Min(0f),Tooltip("冲刺速度")]
    public float sprintSpeed = 15;
    [Min(0f),Tooltip("冲刺时间")]
    public float sprintTime = 0.2f;
    public float sprintCDTime { get; private set; }
    [Min(1),Tooltip("冲刺次数")]
    public int maxSprintCount = 2;
    [Min(0),Tooltip("当前冲刺次数")]
    public int currentSprintCount;
    [Header("冲刺颜色")]
    [Tooltip("冲刺时的颜色")]
    public Pair<SpriteRenderer,Color>[] rushSpColors;
    [Tooltip("恢复冲刺时的颜色")]
    public Pair<SpriteRenderer,Color>[] resetRushSpColors;
    [Tooltip("特效")]
    public BlackRushFx rushFx;
    [Min(0.1f),Tooltip("产生特效的延迟")]
    public float spawnFxdelayTime = 1f;
    [Header("冲刺，爬墙跳功能禁止")]
    public bool banFunctions = true;
    [Header("子弹滑行Lock")]
    [Tooltip("站在子弹上后锁定相对位置时长")]
    public float lockBtTime = 0.1f;
    [Header("检测")]
    [Tooltip("地面检测点的位置，通常为挂在Player下的一个空物体，位于Player脚下的位置")]
    public Transform groundCheckTransform;
    [Tooltip("检测点的大小")]
    public float groundCheckRange=0.1f;
    [Tooltip("地面的检测层")]
    public LayerMask groundLayerMask;
    [Tooltip("墙面监测点位置，通常为挂在Player下的一个空物体，位于Player左右手的位置")]
    public Transform wallCheckTransform;
    [Tooltip("墙面的检测层")]
    public LayerMask wallLayerMak;
    bool isWall;

    //速度 += Extern
    [HideInInspector]
    public float leftRushSpeedExtern, rightRushSpeedExtern, upRushSpeedExtern, downRushSpeedExtern;
    [HideInInspector]
    public float leftMoveSpeedExtern, rightMoveSpeedExtern;

    public bool isLockedMove { get; private set; }
    public bool isLockedRush { get; private set; }
    public bool isLockedJump { get; private set; }
    public bool isFaceLeft { get; private set; }
    public bool isGround { get; private set; }

    BlackAnim m_anim;
    float m_horizontal,m_vertical;
    float m_jumpSpeed;  //根据gravityScale和jumpSpeed确定正确的速度，保证gravityScale改变时也可以不改speed
    bool m_canJump;
    bool m_isInputJump;
    bool m_canSprint;
    bool m_canMove;
    bool m_sprintCD;
    bool m_controlMove;
    bool doubleJump;
    bool doubleSprint;
    float m_gravityScaleKeep;
    Rigidbody2D m_rigidbody2D;
    Quaternion m_qtLeft = Quaternion.Euler(0f,180f,0f);
    Quaternion m_qtRight = Quaternion.identity;
    Vector2 Sprint_velocity;
    Vector3 BulletTouchedPosition;
    BulletCommon Object;
    float m_lockmovetime = 0f;
    float m_lockrushtime = 0f;
    float m_lockjumptime = 0f;
    float m_lockBTtime = 0f;
    bool m_forcelock = false;
    float m_fxspawntime;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.prefabName() == "Bullet")
        {
            m_lockBTtime = 0f;
            Object = other.gameObject.GetComponent<BulletCommon>();
            if(Object != null)
            {
                BulletTouchedPosition = other.transform.position - transform.position;
            }
            m_rigidbody2D.velocity = Vector2.zero;
        }
    }
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.prefabName() == "Bullet")
        {
            if(m_lockBTtime <= lockBtTime)m_lockBTtime += Time.deltaTime;
            if(Object != null)
            {
                if(transform.position.y > other.transform.position.y && m_lockBTtime <= lockBtTime)
                transform.position = other.transform.position - BulletTouchedPosition;
            }
        }
    }
    public string GetExternSpeedString()
    {
        return "Extern_ leftMove: " + leftMoveSpeedExtern + " rightMove: " + rightMoveSpeedExtern + " " +
        "leftRush: " + leftRushSpeedExtern + " rightRush: " + rightRushSpeedExtern + " upRush: " + upRushSpeedExtern + " downRush: " + downRushSpeedExtern;
    }

    public void ResetRushCount()
    {
        currentSprintCount = maxSprintCount;
        UColor.ChangeColor(resetRushSpColors);
    }

    public void StopRush()
    {
        m_sprintCD=false;
        doubleSprint=true;
        sprintCDTime=sprintTime;
        m_rigidbody2D.velocity=Vector2.zero;
        m_rigidbody2D.gravityScale = m_gravityScaleKeep;
        m_anim.Rush(false);
    }

    public void LockRush(float time,bool reset = false)
    {
        isLockedRush = true;
        m_lockrushtime = reset ? time : m_lockrushtime + time;
        StopRush();
    }

    public void LockMove(float time,bool reset = false)
    {
        isLockedMove = true;
        m_lockmovetime = reset ? time : m_lockmovetime + time;
    }

    public void LockJump(float time,bool reset = false)
    {
        isLockedJump = true;
        m_lockjumptime = reset ? time : m_lockjumptime + time;
    }

    public void LockAll(float time,bool reset = false)
    {
        LockMove(time,reset);
        LockRush(time,reset);
        LockJump(time,reset);
    }

    private void LockUpdate()
    {
        //计时器
        if(m_forcelock) { return; }
        float dt = Time.deltaTime;
        if(isLockedMove)
        {
            m_lockmovetime -= dt;
            if(m_lockmovetime < 0f)  { isLockedMove = false; }
        }
        if(isLockedRush)
        {
            m_lockrushtime -= dt;
            if(m_lockrushtime < 0f)  { isLockedRush = false;  }
        }
        if(isLockedJump)
        {
            m_lockjumptime -= dt;

            if(m_lockjumptime < 0f)  { isLockedJump = false; }
        }
    }

    private void RushFxUpdate()
    {
        if( rushFx!=null && m_sprintCD)
        {
            m_fxspawntime -= Time.deltaTime;
            if(m_fxspawntime<0f)
            {
                m_fxspawntime = spawnFxdelayTime;
                GameObject.Instantiate(rushFx,transform.position,transform.rotation);
            }
        }
    }

    // 秒针碰撞主角的逻辑
    public void ClockCollision(int Scale)  // 输入参数为当前秒针转动方向，用于控制给予力的方向
    {
        Vector2 temp = new Vector2(0,0);
        switch (Scale)
        {
            case 0:
                temp.x = clockSpeed;
                temp.y = -clockSpeed;
                break;
            case 1:
                temp.x = -clockSpeed;
                temp.y = -clockSpeed;
                break;
            case 2:
                temp.x = clockSpeed;
                temp.y = clockSpeed;
                break;
            default:
                break;
        }
        m_rigidbody2D.velocity = temp;
    }
    //左右移动：MovePosition(...)
    //跳跃：向上速度*当前重力比例
    //冲刺：忽略重力，用(冲刺速度+extern)*time移动
    void Start()
    {
        m_anim = GetComponent<BlackAnim>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        sprintCDTime = sprintTime;
        m_gravityScaleKeep  =  m_rigidbody2D.gravityScale;
        m_jumpSpeed = jumpSpeed * Mathf.Sqrt(m_gravityScaleKeep);
        Sprint_velocity = Vector2.zero;
        doubleSprint=true;
        
        //滤镜，时停时间
        if(banFunctions) 
        { 
            BulletTime.instance.bulletTime = Mathf.Infinity;
            BulletTime.instance.cd = 0f;
            FindObjectOfType<NumberShower>()?.gameObject.SetActive(false);
            GLOBAL.instance.useFilter = false;
        }
        else 
        {
            GLOBAL.instance.useFilter = true;
            PositionConstraint constraint = FindObjectOfType<NumberShower>()?.GetComponent<PositionConstraint>();
            if(constraint!=null)
            {
                ConstraintSource source = new ConstraintSource();
                source.sourceTransform = transform;
                source.weight = 1;
                if(constraint.sourceCount==0)
                {
                    constraint.AddSource(source);
                }
                else 
                {
                    constraint.SetSource(0,source);
                }
            }
        }

        //相机设置
        var virualcamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        if(virualcamera!=null) { virualcamera.Follow = transform; }

        //角色死亡，场景重载
        Health health = GetComponent<Health>();
        if(health!=null)
        {
            health.E_Die += (GameObject inst)=>{
                m_forcelock = true;
                LockAll(Mathf.Infinity,true);
                m_anim.Die();
                GLOBAL.instance.GameEnd();
            };
        }
    }

    void Update()
    {
        GroundCheck();
        InputCheck();
        WallCheck();
        if(!banFunctions) { WallJump(); Skill_BulletTime(); }
        LockUpdate();
        RushFxUpdate();
    }
    void FixedUpdate()
    {
        Movement();
        Jump();
        if(!banFunctions){ Sprint(); }
    }
    void Jump()
    {
        if(isLockedJump) {return;}
        if(m_canJump && !m_sprintCD && currentJumpCount > 0)
        {
            if(banFunctions)
            {
                // BulletTime.ResetSkill();
                // BulletTime.instance.StartBulletTime();
                m_rigidbody2D.velocity = new Vector2(0,m_jumpSpeed);
                
            }
            else
            {
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x,m_jumpSpeed);
            }
            m_canJump=false;
            currentJumpCount -= 1;
            m_anim.Jump();
            AudioManager.PlayAudio("角色-跳跃-起跳3");
        }
    }
    void Movement()
    {
        if(isLockedMove) {return;}
        m_canMove = !Mathf.Approximately (m_horizontal, 0f);
        if(!m_canMove) { m_anim.Move(false); }
        if(m_canMove&&!m_sprintCD&&m_controlMove)
        {
            isFaceLeft = m_horizontal < 0f;
            m_canMove = false;
            m_rigidbody2D.position += new Vector2(m_horizontal * Time.deltaTime * ( moveSpeed + (isFaceLeft? leftMoveSpeedExtern : rightMoveSpeedExtern) ),0f);
            m_anim.Move(true);
            transform.rotation = isFaceLeft ? m_qtLeft : m_qtRight;
        }
    }
    void Sprint()
    {
        if(isLockedRush) { return; }
        if(!m_controlMove) { return; }
        if(m_canSprint && currentSprintCount > 0)
        {
            --currentSprintCount;
            m_canSprint=false;
            m_sprintCD=true;
            sprintCDTime = sprintTime;
            m_gravityScaleKeep = m_rigidbody2D.gravityScale;
            m_rigidbody2D.gravityScale = 0f;
            doubleSprint=false;
            m_anim.Rush(true);
            UColor.ChangeColor(rushSpColors);
            m_fxspawntime = 0f;
        }
        if(m_sprintCD)
        {
            m_rigidbody2D.gravityScale = 0f;
            AudioManager.PlayAudio("角色-冲刺1");
            sprintCDTime -= Time.fixedDeltaTime;
            if(sprintCDTime >= sprintTime - 0.1f) {Sprint_velocity = new Vector2(m_horizontal,m_vertical); }
            if(Sprint_velocity.x < 0|| (Sprint_velocity.y==0 && transform.rotation.y<0))
                Sprint_velocity.x = -sprintSpeed - leftRushSpeedExtern;
            else if(Sprint_velocity.x > 0 || (Sprint_velocity.y==0 && transform.rotation.y==0))
                Sprint_velocity.x = sprintSpeed + rightRushSpeedExtern;
            if(Sprint_velocity.y < 0)
                Sprint_velocity.y = -sprintSpeed - downRushSpeedExtern;
            else if(Sprint_velocity.y > 0)
                Sprint_velocity.y = sprintSpeed - upRushSpeedExtern;

            m_rigidbody2D.velocity = Sprint_velocity;//*(Mathf.Log(sprintCDTime/sprintTime+1.5f)+0.3f);

            if(sprintCDTime<=0)
            {
                doubleSprint=true;
                m_rigidbody2D.velocity=Vector2.zero;
            }
            if(sprintCDTime<-0.1f)
            {
                m_sprintCD=false;
                m_anim.Rush(false);
                m_rigidbody2D.gravityScale = m_gravityScaleKeep;
                sprintCDTime=sprintTime;
            }
        }
    }
    void WallJump()
    {
        if(isWall&&INPUT.climb)
        {
            m_rigidbody2D.gravityScale=0f;
            if(INPUT.jump)
            {
                m_controlMove = false;
                m_rigidbody2D.velocity = Vector2.zero;
                m_rigidbody2D.velocity = new Vector2(transform.right.x*moveSpeed*-1,jumpSpeed*2);
                if(transform.rotation.y<0)
                {
                    transform.rotation=m_qtRight;
                }else
                {
                    transform.rotation=m_qtLeft;
                }
            }else
            {
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x,0);
            }
        }else
        {
            m_rigidbody2D.gravityScale=m_gravitySacleDown;
        }
    }

    void Skill_BulletTime()
    {
        if(INPUT.bulletTime.value && !BulletTime.isBulletTime)
            BulletTime.instance.StartBulletTime(); 
    }

    void InputCheck()
    {
        if(INPUT.jump&&isGround)
        {
            m_canJump=true;
        }
        if(INPUT.rush&&doubleSprint)
        {
            m_canSprint=true;
        }
        m_horizontal = INPUT.horizontal.value;
        m_vertical = INPUT.vertical.value;
    }
    void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheckTransform.position,groundCheckRange,groundLayerMask);
        m_anim.isGrounded = isGround;
        // if(isGround && !m_sprintCD)
        // {
        //     ResetRushCount();
        // }
        // if(isGround && m_rigidbody2D.velocity.y <= 0)
        // {
        //     if(banFunctions) { BulletTime.EndBulletTime(); }
        //     currentJumpCount = maxJumpCount;
        // }
        if(isGround)
        {
            if(currentJumpCount < maxJumpCount || !m_sprintCD && currentSprintCount < maxSprintCount)
                AudioManager.PlayAudio("角色-跳跃-落地");
            if(!m_sprintCD) {  ResetRushCount(); }
            if(m_rigidbody2D.velocity.y <= 0f) { currentJumpCount = maxJumpCount; }
            if(banFunctions && BulletTime.isBulletTime) { BulletTime.EndBulletTime(); }
        }
        else
        {
            if(banFunctions && !BulletTime.isBulletTime) { BulletTime.instance.StartBulletTime();}
        }
    }
    void WallCheck()
    {
        isWall=Physics2D.OverlapCircle(wallCheckTransform.position,groundCheckRange,wallLayerMak);
        if(isWall)
        {
            m_anim.isGrounded = isWall; // 在墙上时播放地面动画
            currentJumpCount = maxJumpCount;
        }else
        {
            m_controlMove=true;
        
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheckTransform.position,groundCheckRange);
        Gizmos.DrawSphere(wallCheckTransform.position,groundCheckRange);
    }
}