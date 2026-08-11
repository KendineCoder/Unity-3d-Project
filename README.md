# **Wand and Sword**
**Sürüm:** 1.1.0  
**Tarih:** 11 Ağustos 2026  
**Durum:** Prototip / Geliştirme Aşaması  
**Geliştirici / Takım:**

---

## 1. Genel Bakış (Overview)

### 1.1 Yüksek Seviye Konsept (High Concept)
*Mount & Blade* tarzı 4 yönlü yakın dövüş mekanikleri ile *Arx Fatalis* tarzı asa bazlı rün çizim sistemini **Third-Person (Üçüncü Şahıs)** kamera açısıyla birleştiren; bünyesinde ışık, ayna ve portal odaklı bulmacalar, prosedürel labirentler,dinamik envanter yönetimi ve tarafsız bir şeytan şehri barındıran 3D Aksiyon RPG.

### 1.2 Oyun Türü ve Platform
* **Tür:** Üçüncü Şahıs Aksiyon RPG / Dungeon Crawler (Third-Person Action RPG)
* **Hedef Platform:** PC (Steam)
* **Hedef Kitle:** Zorlu, ustalaşma gerektiren dövüş sistemlerini seven; büyü ve keşif kombinasyonlarıyla özgür ve hareketli oynanış arayan 16-35 yaş arası oyuncular.

### 1.3 Benzersiz Satış Noktaları (USP)
* **[Özellik 1] Asa ile Rün Çizimi ve Fiziksel Büyüler:** Ekrana çizilerek yapılan büyüler sadece doğrudan hasar vermez; portal açma, nesneleri yönlendirme ve bulmaca çözme gibi çevresel etkileşimlere sahiptir.
* **[Özellik 2] Portal ve Işık-Ayna Tabanlı Çevre Bulmacaları:** Labirentteki kilitli kapıları ve antik mekanizmaları açmak için ışık kaynaklarını, taşınabilir aynaları ve Portal büyüsünü kullanarak ışık huzmesini hedef kristale ulaştırma mekaniği.
* **[Özellik 3] Oyuncu Pattern'lerini Öğrenen Adaptif Boss'lar:** Boss'lar oyuncunun sürekli kullandığı yönlü saldırıları veya rün alışkanlıklarını analiz ederek dövüş esnasında kendi stratejilerini değiştirir.
* **[Özellik 4] Terk Edilmiş Şeytan Sığınağı (Güvenli Bölge / Hub):** Labirentte tıkılı kalmış, savaştan bıkmış tarafsız şeytanların kurduğu yerleşkede ticaret yapılabilir, yeni rünler öğrenilebilir ve ekipmanlar geliştirilebilir, ticaret yapılabilir.
* **[Özellik 5] Dinamik & Prosedürel Labirent Yapısı:** Ana hikaye alanları ve Şeytan Şehri el yapımı özel alanlardan oluşurken, bu alanlar arasındaki koridorlar ve zindanlar prosedürel olarak üretilir.
* **[Özellik 6] Glider (Süzülme Aracı):** Karakter, Glider sayesinde labirenti yüksekten gözlemleyip rotasını çizebilir, ışık bulmacalarının çözümlerini yukarıdan analiz edebilir veya akrobatik hava hareketleri yapabilir. Telekinezi büyüsü ile birleştirilip düşmanlara karşı silah olarak kullanılabilir

---

## 2. Oynanış ve Mekanikler (Gameplay & Mechanics)

### 2.1 Temel Döngü (Core Loop)
<img width="1012" height="186" alt="resim" src="https://github.com/user-attachments/assets/e3b82bac-c495-4d7b-9180-326b03d0db7e" />


### 2.2 Oyuncu Kontrolleri

| Tuş (PC) | Gamepad | Eylem |
| :--- | :--- | :--- |
| **WASD** | Sol Analog | Karakter Hareketi | Glider Hareketi
| **Mouse** | Sağ Analog | Kamera Kontrolü / Hedefleme |
| **Sol Tık + Fare Yönü** | RB / RT | Yönlü Yakın Dövüş Saldırısı (Farenin çekildiği yöne doğru) |
| **Sağ Tık + Fare Yönü (Basılı Tutma)**| LB / LT | Yönlü Blok / Parry Duruşu |
| **Scrollwheel** | Y / Triangle | Asa İle Rün Çizim Modunu Açma |
| **E** | X / Square | Etkileşim / Ayna Döndürme / Nesne Tutma (Telekinezi ile) |

### 2.3 Ana Mekanikler

#### A. Third-Person 4 Yönlü Kılıç Dövüşü (Directional Combat)
* **Perspektif ve Görüş:** Kamera karakterin arkasında durur. Karakterin silah tutuşu ve vücut duruşu, seçilen saldırı/blok yönünü (Sağ, Sol, Yukarı, Aşağı) görsel olarak net biçimde yansıtır.
* **Saldırı/Blok Yönleri:** Fare hareketindeki değişim ile seçilen 4 ana yön doğrultusunda kılıç savrulur veya blok tutulur.

#### B. Asa, Rün Çizimi ve Portal Büyüleri (Rune-Casting & Spatial Magic)
Büyü çizimi ekran ortasında gerçekleşir:
* **Portal Büyüsü:** Görüş alanındaki yüzeylere birbirine bağlı iki portal açar. 
  * *Aksiyon Kullanımı:* Labirent alanlarında hızlı seyahat ve tuzak aksiyon imkanı sunar.
  * *Bulmaca Kullanımı:* Işık huzmelerini portalın içine bükerek, duvarların arkasındaki veya erişilemez odalardaki ayna/hedef sistemlerine ışığı iletmek için kullanılır.
* **Telekinezi Büyüsü:** Çevredeki ağır ayna bloklarını veya nesneleri tutarak doğru pozisyona taşımayı ve yerleştirmeyi sağlar.
* **Elementel Büyüler:** Alan etkili (AoE) ateş, buz ve yıldırım rünleri.

#### C. Bulmaca Sistemi: Işık, Ayna ve Portal Yönlendirme (Light & Portal Puzzles)
* **Temel Amaç:** Odadaki/zindandaki ana ışık kaynağından (Kristal Fener) çıkan sürekli ışık huzmesini, engelleri aşarak labirentin sonundaki veya kapı mekanizmasındaki hedef kristale ulaştırmak.
* **Bulmaca Elemanları:**
  * **Sabit/Dönebilir Aynalar:** Etkileşim tuşu (E) ile açıları değiştirilerek ışığı 90 veya 45 derecelik açılarla yansıtan yapılar.
  * **Taşınabilir Ayna Blokları:** Telekinezi büyüsü veya fiziksel güçle yeri değiştirilebilen dinamik aynalar.
  * **Portal Entegrasyonu:** Doğrudan görüş açısında olmayan ya da kalın duvarların arkasında kalan alanlara ışığı aktarmak için portal yüzeylerinin kullanılması.
  * **Işık Kırıcılar ve Renk Filtreleri:** Işık huzmesini ikiye bölen krizantem kristalleri veya kapı kilitlerinin rengine göre renk değiştiren filtreler.
* **Keşif ve Perspektif:** Oyun alanının yapısı sayesinde oyuncu Glider ile havalanarak ışığın izlediği rotayı ve ayna açısını yüksekten gözlemleyebilir.

#### D. Can & Mana Sistemi
* **Maksimum Can:** 100 (Ölüm durumunda karakter ölümsüz olduğu için zindanın son checkpoint noktasında yeniden doğar).
* **Mana:** Büyü kullanımı mana harcar. Mana zamanla dolmaz; katledilen canavarlardan düşen kristaller toplanarak veya bulmaca çözümlerinden elde edilen antik enerjilerle yenilenir.

#### E. Şeytan Yerleşkesi ve Kristal Ekonomisi
* **Tarafsız Şeytanlar (Neutral Demon Remnants):** Büyük savaştan sonra labirentte mahsur kalan eski şeytan askerleri. Oyuncuya saldırmazlar; kahramanın Daidalos'u yenip labirenti yok etmesini kendi özgürlükleri için tek şans olarak görürler.
* **Kristal Kullanım Alanları:**
  * **Anlık Mana Yenileme:** Dövüş ve bulmaca esnasında büyü yapabilmek için kullanılır.
  * **Ticaret:** Şeytan şehrinde harcanarak yeni rün formülleri, kılıç geliştirmeleri, zırh/görsel özelleştirmeler ve labirent bulmacaları için ipucu haritaları satın alınır.

---

## 3. Hikaye ve Dünya (Lore & World)

### 3.1 Özet Hikaye
Dünyayı büyük bir felaketten kurtaran kahramanımız, zamanla kazandığı muazzam güç nedeniyle bizzat kendisi bir tehdide dönüşür. Yendiği tüm iblislerden daha korkutucu hale gelen kahramanın bu gücünden çekinen yakın dostu **Daidalos**, onu yeryüzünün altındaki sonu gelmeyen bir karanlığa, bir hapishane-labirente hapseder. 

Ancak kahraman labirentte yalnız değildir. Yıllar önce yenilgiye uğrattığı Şeytan Ordusu'nun savaştan bıkmış kalıntıları, Daidalos'un gazabından kaçarak labirentin derinliklerinde gizli bir yerleşke kurmuştur. Bu tarafsız şeytanlar, kahramanı bir düşman olarak değil, kendilerini bu sonsuz hapishaneden kurtarabilecek **tek ortak müttefik** olarak görürler. Kahraman ölümsüzdür; tek amacı ışık kilitlerini çözüp labirentten çıkmak ve Daidalos ile yüzleşmektir.

### 3.2 Karakterler
* **Ana Karakter (Protagonist):** Yeryüzünün eski koruyucusu, yaşayan en güçlü sihirli kılıç ustası. Ölümsüzlük lanetine sahip olduğu için labirentte her öldüğünde yeniden doğar. Labirentin kalbine ışık götürüp kaçmayı amaçlar.
* **Daidalos (Antagonist):** Kahramanın eski dostu, dahi bir mucit ve mimar. Dostunu bir canavara dönüşmeden önce durdurduğuna inanır ve bu sonsuz labirenti onu içeride tutmak için tasarlamıştır.
* **Tüccar Şeytanlar (NPC'ler):**
  * *Maden (Silahkar Şeytan):* Kılıcı ve yakın dövüş kabiliyetlerini geliştirir.
  * *Irsus (Rün Mimarı Şeytan):* Kristallere karşılık asayla çizilebilecek yeni rün formüllerini öğretir.
  * *Lagros (Eski Komutan Şeytan):* Labirentin yapısı ve Daidalos'un boss'ları hakkında stratejik ipuçları ve değerli pasif etki verir.

---

## 4. Görsel ve İşitsel Stil (Art & Audio)

### 4.1 Görsel Stil
* **Sanat Tarzı:** Stylized Low Poly / Cel-shaded (Zelda: Breath of the Wild & Genshin Impact estetiği). Karakter animasyonları Third-Person kamera açısında akıcı ve belirgin siluetlere sahiptir.
* **VFX (Görsel Efektler):** Aynalardan sekerek odayı aydınlatan parlak, renkli ışık huzmeleri; portalların kenarlarındaki büyülü akışkan efektler ve rün çizim izleri.
* **Renk Paleti:** Karanlık antik harabeler içerisinde yüksek kontrast oluşturan parıltılı kristaller, sarı/mavi ışık huzmeleri ve dikey kanyon aydınlatmaları.
* **Referanslar:** *The Legend of Zelda: Breath of the Wild*, *Arx Fatalis*, *Mount & Blade*.

### 4.2 Ses ve Müzik
* **Müzik:** Atmosferik Epik/Akustik Müzikler. Keşif anlarında minimalist flüt ve telli çalgılar; boss dövüşlerinde tempolu, orkestral tınılar.
* **Ses Efektleri (SFX):** Kılıç çarpışma sesleri, rün çizerken çıkan büyülü parıltı efektleri, Glider açılma ve süzülme rüzgar sesleri, fırlatılan nesnelerin fiziksel darbe sesleri.

---

## 5. Kullanıcı Arayüzü (UI / UX)

### 5.1 HUD (Heads-Up Display)
* **Sol Üst:** Can Barı ve Toplanan Kristal / Mana Göstergesi.
* **Karakter Etrafı / Sağ Alt:** Omuz hizasında duran dinamik 4 yönlü kılıç yön göstergesi (Saldırı veya blok hazırlığındayken görünür).

---

## 6. Teknik Gereksinimler & Yapı (Technical Specs)

* **Oyun Motoru:** Unity 2022.3 LTS 
* **Hedef Kare Hızı:** 60 FPS (Sabit)
* **Temel Sistemler:** 
  * **Light Raycast & Reflection System:** Işığın aynalardan sekmesini, açısını ve Portal ile temas ettiğinde diğer taraftan aynı açıyla çıkmasını sağlayan modüler fizik/raycast mimarisi.
  * Third-Person Karakter Kontrolcüsü (Gelişmiş animasyon harmanlama / Animation Blending).
  * Dinamik Kamera Sistemi (Engellere takılmayan SpringArm / Cinemachine Collider).
  * Fizik tabanlı nesne etkileşimi (Rigidbody).
  * 2D Fare çizimini 3D büyü koduna çeviren Rün Tanıma Algorithması.
  * Prosedürel Zindan Oluşturucu (Proc-Gen Dungeon Algorithm).
  * Adaptif Boss Yapay Zekası (Counter-State Machine).
* **Sürüm Kontrolü:** GitHub / Git LFS

---

## 7. Gelecek Planları ve Kapsam Dışı (Out of Scope)

### Şimdilik Hariç Tutulanlar
* Multiplayer / Online modlar (Sadece Tek Oyunculu).
* Dış dünyada geçen geniş açık dünya haritaları (Oyun tamamen Labirent ve Şeytan Yerleşkesi odaklıdır).
* Karmaşık diyalog seçim ağaçları (NPC'ler sadece görev vermek, hikaye anlatmak ve ticaret yapmak içindir).
