using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _controller;


    [SerializeField]
    private float _speed = 3f;
    private float _gravity = 9.81f;

    [SerializeField]
    private GameObject _muzzleFlashPrefab;

    [SerializeField]
    private GameObject _hitMarketPrefab;

    private AudioSource _audioSource;

    [SerializeField]
    private int _currentAmmo;

    private int _maxAmmo = 500;

    private bool isReloading = false;

    public bool hasCoin = false;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _audioSource = GetComponent<AudioSource>();

        _currentAmmo = _maxAmmo;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentAmmo > 0 && Input.GetMouseButton(0))
        {
            Shoot();
        }
        else
        {
            _muzzleFlashPrefab.SetActive(false);
            _audioSource.Stop();
        }
        CalculeMovement();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = _maxAmmo;
        isReloading = false;
        _uiManager.UpdateAmmo(_currentAmmo);
    }

    void Shoot()
    {

        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        _currentAmmo--;
        _uiManager.UpdateAmmo(_currentAmmo);
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
            _muzzleFlashPrefab.SetActive(true);
        }
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            GameObject _hit = Instantiate(_hitMarketPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(_hit, 1f);
        }


        if (Input.GetMouseButtonUp(0))
        {
            _muzzleFlashPrefab.SetActive(false);
            _audioSource.Stop();
        }


    }

    void CalculeMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity);

        _controller.Move(velocity * Time.deltaTime);
    }
}
