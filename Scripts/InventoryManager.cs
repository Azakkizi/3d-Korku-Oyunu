using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class InventoryManager : MonoBehaviour
{
    //ENVANTER YÖNETİMİ

    // Oyun boyunca aynı envantere her zaman erişebilmek için nesne oluşturma
    public static InventoryManager Instance;
    // Eşyaların listeleneceği list öğesini oluşturma
    public List<Item> Items = new List<Item>();
    // Envanter içeriği - Unity Canvas öğesi, bu öğe içinde gridal bir sistemde eşyalar dizilecek
    public Transform ItemContent;
    // Envanterdeki her öğe için oluşturulan nesne
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    // Envantere öğe ekleme
    public void Add(Item item)
    {
        Items.Add(item);
    }
    // Envanterden öğe çıkarma
    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    // Öğeleri listeleme
    public void ListItems()
    {
        // Her envanter açılışında eşyalar kaydedildiği için kopyalanır. 
        // Bunu önlemek için envanteri her açtığımızda bu öğeleri baştan silmemiz gerekir
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        // UnityEngine'in Object class'ında tanımlanmış olan Instantiate fonksiyonu sayesinde 
        // bir nesneyi döngü içinde kopyalayarak herbirine kendine özgü özellik atayabiliriz
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            //var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            //itemName.text = item.name;
            itemIcon.sprite = item.icon;
        }
    }

    // ENVANTER AÇMA-KAPAMA

    // Envanter nesnesi oluşturma - Unity'de bu nesneye Canvas'ta oluşturulan arayüz görseli atanacak
    public GameObject Inventory;
    // Envanterin kapalı mı açık mı olduğunu kontrol eden boolean değer
    public bool inventoryIsClosed;

    void Start()
    {
        // Oyun başladığında envanter kapalığı olacağı için bu boolean değer true olur
        inventoryIsClosed = true;
        // Oyun başladığında envanter arayüzü kapalı başlar
        Inventory.SetActive(false);
    }

    void Update()
    {
        // Oyun boyunca her "I"ya basıldığında bu if bloğu çalışacak
        if(Input.GetKeyDown(KeyCode.I))
        {
            // Eğer envanter kapalıysa
            if(inventoryIsClosed == true)
            {
                // Eğer envanter boş değilse eşyaları listele
                if(Items != null)
                {
                    ListItems();
                }
                // Eğer boşsa sadece envanter arayüzünü boş göster
                Inventory.SetActive(true);
                // Envanter kapalı olmayacağı için kontrolü yanlış döndür
                inventoryIsClosed= false;
                // Fare imlecini göster
                ShowMouseCursor();
            }
            // Eğer envanter açıksa
            else
            {
                // Envanter arayüzünü kapat
                Inventory.SetActive(false);
                // Envanter kapandığı için kontrolü doğru döndür
                inventoryIsClosed= true;
                // Fare imlecini gizle
                HideMouseCursor();
            }
        }
    }

    // Envanter açılınca fare ile işlem yapılacağı için imlecin görünmesi fonksiyonu
    public void ShowMouseCursor()
    {
        // Envanter açıldığında oyuncu nesnesini etkisiz hale getirerek oynamasını önleme
        GameObject.FindGameObjectWithTag("Player").transform.GetComponent<FirstPersonController>().enabled = false;
        // İmleci görünür yapma
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    // Envanter ile işlem bittiğinde normal oyuna dönmek için imleci görünmez yapma
    public void HideMouseCursor()
    {
        // Oyuna geri dönüşte oyuncu nesnesinin hareketlerini tekrar etkin hale getirme
        GameObject.FindGameObjectWithTag("Player").transform.GetComponent<FirstPersonController>().enabled = true;
        // İmleci görünmez yapma
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
