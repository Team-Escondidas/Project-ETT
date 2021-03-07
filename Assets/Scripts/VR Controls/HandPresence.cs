using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public List<GameObject> controllerPrefabs;
    private InputDevice targetDevice;
    private GameObject spawnedController;
    // Start is called before the first frame update
    void Start()
    {
        getCurrentDevice();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetDevice.name == null)
        {
            getCurrentDevice();
        }
        else
        {
            targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool primaryButtonValue);
            if (primaryButtonValue)
                Debug.Log("Pressing Primary Button.");
        }

    }


    private void getCurrentDevice()
    {
        if (XRSettings.loadedDeviceName.Length != 0)
        {
            Debug.Log(XRSettings.loadedDeviceName);
            var devices = new List<InputDevice>();
            var rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
            foreach (var item in devices)
            {
                Debug.Log(item.name + item.characteristics);
            }

            if(devices.Count > 0 && devices[0].name.Length > 0)
            {
                targetDevice = devices[0];
                Debug.Log("Succesfully found Device: " + targetDevice.name);
                GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
                if(prefab)
                {
                    spawnedController = Instantiate(prefab, transform);
                }
                else
                {
                    Debug.Log("Did not find controller {targetDevice.name}");
                    spawnedController = Instantiate(controllerPrefabs[0], transform);
                }
            }
        }
    }
}
