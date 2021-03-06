﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public AnimationCurve chanceFromHealth;

    [Header("Door")]
    public Animation anim;
    public GameObject[] doors;
    public GameObject closeDoor;

    [Header("Enemy")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawn;

    [Header("Powerups")]
    public GameObject[] pot;

    [HideInInspector] public List<GameObject> enemis;

    private RoomsOptions options;
    private bool spawned;
    private void Awake()
    {
        options = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsOptions>();
    }

    private void Start()
    {
        options.rooms.Add(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !spawned)
        {
            spawned = true;
            float chance = chanceFromHealth.Evaluate(collision.GetComponent<PlayerHealth>().currentHealth) * 100;
            if(enemySpawn.Length != 0 || enemySpawn != null)
            foreach (Transform spawner in enemySpawn)
            {
                float rand = Random.Range(0, 100);
                if (rand <= chance)
                {
                    Instantiate(pot[Random.Range(0, pot.Length)], spawner.position, Quaternion.identity);
                }
                else
                {
                    GameObject enemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawner.position, Quaternion.identity);
                    enemy.transform.parent = transform;
                    enemis.Add(enemy);
                }
                Destroy(spawner.gameObject);
            }
            StartCoroutine(CheackEnemies());
        }
        if(collision.CompareTag("boss"))
        {
            enemySpawn = null;
        }
        if(collision.CompareTag("boss"))
        {
            collision.gameObject.SetActive(true);
        }
    }
    IEnumerator CheackEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemis.Count == 0);
        OpenDoors();
    }
    public void OpenDoors()
    {
        foreach(GameObject door in doors)
        {
            if(door != null && door.transform.childCount != 0)
            {
                door.GetComponent<Animator>().SetBool("isOpen", true);
            }
        }
    }
}
