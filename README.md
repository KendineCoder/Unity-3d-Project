**Sürüm:** 1.0.0  
**Tarih:** 11 Ağustos 2026  
**Durum:** Prototip / Geliştirme Aşaması  
**Geliştirici / Takım:** 

---

## 1. Genel Bakış (Overview)

### 1.1 Yüksek Seviye Konsept (High Concept)
*Mount & Blade* tarzı 4 yönlü yakın dövüş mekanikleri ile *Arx Fatalis* tarzı asa bazlı rün çizim sistemini **Third-Person (Üçüncü Şahıs)** kamera açısıyla birleştiren; bünyesinde fizik odaklı bulmacalar, prosedürel labirentler ve tarafsız bir şeytan şehri barındıran 3D Aksiyon RPG.

### 1.2 Oyun Türü ve Platform
* **Tür:** Üçüncü Şahıs Aksiyon RPG / Dungeon Crawler (Third-Person Action RPG)
* **Hedef Platform:** PC (Steam)
* **Hedef Kitle:** Zorlu, ustalaşma gerektiren dövüş sistemlerini seven; karakter görüşü, dikey hareketlilik, büyü ve keşif kombinasyonlarıyla özgür oynanış arayan 16-35 yaş arası oyuncular.

### 1.3 Benzersiz Satış Noktaları (USP)
* **[Özellik 1] Asa ile Rün Çizimi ve Fiziksel Büyüler:** Ekrana çizilerek yapılan büyüler sadece doğrudan hasar vermez; nesneleri fırlatma, portal açma gibi fiziksel ve çevresel etkileşimlere sahiptir.
* **[Özellik 2] Oyuncu Pattern'lerini Öğrenen Adaptif Boss'lar:** Boss'lar oyuncunun sürekli kullandığı yönlü saldırıları veya rün alışkanlıklarını analiz ederek dövüş esnasında kendi stratejilerini değiştirir.
* **[Özellik 3] Terk Edilmiş Şeytan Sığınağı (Güvenli Bölge / Hub):** Labirentte tıkılı kalmış, savaştan bıkmış tarafsız şeytanların kurduğu yerleşkede ticaret yapılabilir, yeni rünler öğrenilebilir ve ekipmanlar geliştirilebilir.
* **[Özellik 4] Dinamik & Prosedürel Labirent Yapısı:** Ana hikaye alanları ve Şeytan Şehri el yapımı özel alanlardan oluşurken, bu alanlar arasındaki koridorlar ve zindanlar prosedürel olarak üretilir.
* **[Özellik 5] Glider (Süzülme Aracı) ile Dikey Keşif:** Karakter sırtından açılan Glider sayesinde labirenti yüksekten gözlemleyip rotasını çizebilir, akrobatik hava hareketleri yapabilir veya büyüleriyle bu aracı bir fırlatma nesnesine dönüştürebilir.

---

## 2. Oynanış ve Mekanikler (Gameplay & Mechanics)

### 2.1 Temel Döngü (Core Loop)
<img width="228" height="613" alt="resim" src="https://github.com/user-attachments/assets/deb8a6a3-bac3-4863-abb4-e71b7d92a2c7" />


### 2.2 Oyuncu Kontrolleri

| Tuş (PC) | Gamepad | Eylem |
| :--- | :--- | :--- |
| **WASD** | Sol Analog | Karakter Hareketi |
| **Mouse** | Sağ Analog | Kamera Kontrolü / Hedefleme |
| **Sol Tık + Fare Yönü** | RB / RT | Yönlü Yakın Dövüş Saldırısı (Farenin çekildiği yöne doğru) |
| **Sağ Tık (Basılı Tutma)**| LB / LT | Yönlü Blok / Parry Duruşu |
| **R Tuşu (veya Sol Tık Basılı)** | Y / Triangle | Asa İle Rün Çizim Modunu Açma (Kamera karaktere yaklaşır) |
| **Space** | A / Cross | Zıplama / Glider'ı Açma (Havadayken) |
| **Shift** | B / Circle | Dash / Sıyrılma / Takla |
| **E** | X / Square | Etkileşim / Nesne Tutma (Telekinezi Büyüsü ile) |

### 2.3 Ana Mekanikler

#### A. Third-Person 4 Yönlü Kılıç Dövüşü (Directional Combat)
* **Perspektif ve Görüş:** Kamera karakterin arkasında durur. Karakterin silah tutuşu ve vücut duruşu, seçilen saldırı/blok yönünü (Sağ, Sol, Yukarı, Aşağı) görsel olarak net biçimde yansıtır.
* **Saldırı/Blok Yönleri:** Fare hareketiyle seçilen 4 ana yön doğrultusunda kılıç savrulur veya blok tutulur.

#### B. Asa ve Rün Çizim Büyüleri (Rune-Casting)
Rün modu açıldığında kamera karakterin omuz hizasına yaklaşır (Over-the-shoulder) ve büyü çizimi ekran ortasında gerçekleşir:
* **Telekinezi (Yerçekimi) Büyüsü:** Çevredeki nesneleri Third-Person görüş açısıyla hedefleyip yakalamayı, düşmanlara fırlatmayı veya bulmacalarda taşımayı sağlar.
* **Portal Büyüsü:** Görüş alanındaki duvar veya yüzeylere ışınlanma kapıları açarak dikey labirent alanlarında hızlı seyahat ve aksiyon imkanı sunar.
* **Elementel Büyüler:** Alan etkili (AoE) ateş, buz ve yıldırım rünleri.

#### C. Can & Mana Sistemi
* **Maksimum Can:** 100 (Ölüm durumunda karakter ölümsüz olduğu için zindanın son checkpoint noktasında yeniden doğar).
* **Mana:** Büyü kullanımı mana harcar. Mana zamanla dolmaz; katledilen canavarlardan düşen kristaller toplanarak yenilenir.

#### D. Şeytan Yerleşkesi ve Kristal Ekonomisi
* **Tarafsız Şeytanlar (Neutral Demon Remnants):** Büyük savaştan sonra labirentte mahsur kalan eski şeytan askerleri. Oyuncuya saldırmazlar; kahramanın Daidalos'u yenip labirenti yok etmesini kendi özgürlükleri için tek şans olarak görürler.
* **Kristal Kullanım Alanları:**
  * **Anlık Mana Yenileme:** Dövüş esnasında büyü yapabilmek için toplanır.
  * **Ticaret:** Şeytan şehrinde harcanarak yeni rün formülleri, kılıç geliştirmeleri, zırh/görsel özelleştirmeler ve harita ipuçları satın alınır.

---

## 3. Hikaye ve Dünya (Lore & World)

### 3.1 Özet Hikaye
Dünyayı büyük bir felaketten kurtaran kahramanımız, zamanla kazandığı muazzam güç nedeniyle bizzat kendisi bir tehdide dönüşür. Yendiği tüm iblislerden daha korkutucu hale gelen kahramanın bu gücünden çekinen yakın dostu **Daidalos**, onu yeryüzünün altındaki sonu gelmeyen bir hapishane-labirente hapseder. 

Ancak kahraman labirentte yalnız değildir. Yıllar önce yenilgiye uğratılan Şeytan Ordusu'nun savaştan bıkmış kalıntıları, Daidalos'un gazabından kaçarak labirentin derinliklerinde gizli bir yerleşke kurmıştır. Bu tarafsız şeytanlar, kahramanı bir düşman olarak değil, kendilerini bu sonsuz hapishaneden kurtarabilecek **tek ortak müttefik** olarak görürler. Kahraman ölümsüzdür; tek amacı bu labirentten çıkıp Daidalos ile yüzleşmektir.

### 3.2 Karakterler
* **Ana Karakter (Protagonist):** Yeryüzünün eski koruyucusu, yaşayan en güçlü sihirli kılıç ustası. Third-Person bakış açısı sayesinde zırhı, kılıcı ve asa detayları görünür. Ölümsüzlük lanetine sahip olduğu için labirentte her öldüğünde yeniden doğar.
* **Daidalos (Antagonist):** Kahramanın eski dostu, dahi bir mucit ve mimar. Dostunu bir canavara dönüşmeden önce durdurduğuna inanır ve bu sonsuz labirenti onu içeride tutmak için tasarlamıştır.
* **Tüccar Şeytanlar (NPC'ler):**
  * *Silahkar Şeytan:* Kılıcı ve yakın dövüş kabiliyetlerini geliştirir.
  * *Rün Mimarı Şeytan:* Kristallere karşılık asayla çizilebilecek yeni rün formüllerini öğretir.
  * *Eski Komutan:* Labirentin yapısı ve Daidalos'un adaptif boss'ları hakkında stratejik ipuçları verir.

---

## 4. Görsel ve İşitsel Stil (Art & Audio)

### 4.1 Görsel Stil
* **Sanat Tarzı:** Stylized Low Poly / Cel-shaded (Zelda: Breath of the Wild & Genshin Impact estetiği). Karakter animasyonları Third-Person kamera açısında akıcı ve belirgin siluetlere sahiptir.
* **Renk Paleti:** Canlı ve açık tonlar. Antik harabe yapıları, parlak kristal ışıkları ve dikey kanyonumsu açıklıklar içeren aydınlatmalar.
* **Referanslar:** *The Legend of Zelda: Breath of the Wild*, *Arx Fatalis*, *Mount & Blade*.

### 4.2 Ses ve Müzik
* **Müzik:** Atmosferik Epik/Akustik Müzikler. Keşif anlarında minimalist flüt ve telli çalgılar; boss dövüşlerinde tempolu, orkestral tınılar.
* **Ses Efektleri (SFX):** Kılıç çarpışma sesleri, rün çizerken çıkan büyülü parıltı efektleri, Glider açılma ve süzülme rüzgar sesleri, fırlatılan nesnelerin fiziksel darbe sesleri.

---

## 5. Kullanıcı Arayüzü (UI / UX)

### 5.1 HUD (Heads-Up Display)
* **Sol Üst:** Can Barı ve Toplanan Kristal / Mana Göstergesi.
* **Karakter Etrafı / Sağ Alt:** Omuz hizasında duran dinamik 4 yönlü kılıç yön göstergesi (Saldırı veya blok hazırlığındayken görünür).
* **Rün Çizim Arayüzü:** R tuşuna basıldığında kamera omuz hizasına yaklaşır, ekran hafifçe odaklanır ve imleç büyülü bir izin kalacağı fırçaya dönüşür.

---

## 6. Teknik Gereksinimler & Yapı (Technical Specs)

* **Oyun Motoru:** Unity 2022.3 LTS 
* **Hedef Kare Hızı:** 60 FPS (Sabit)
* **Temel Sistemler:** 
  * Third-Person Karakter Kontrolcüsü (Gelişmiş animasyon harmanlama / Animation Blending).
  * Dinamik Kamera Sistemi (Engellere takılmayan SpringArm / Camera Collision).
  * Fizik tabanlı nesne etkileşimi (Rigidbodyi).
  * 2D Fare çizimini 3D büyü koduna çeviren Rün Tanıma Algoritması (Gestures Recognition).
  * Prosedürel Zindan Oluşturucu (Proc-Gen Dungeon Algorithm).
  * Adaptif Boss Yapay Zekası (Pattern Recognition & Reinforcement Learning).
* **Sürüm Kontrolü:** GitHub / Git LFS

---

## 7. Gelecek Planları ve Kapsam Dışı (Out of Scope)

### Şimdilik Hariç Tutulanlar (Scope Creep Önleme)
* Multiplayer / Online modlar (Sadece Tek Oyunculu).
* Dış dünyada geçen geniş açık dünya haritaları (Oyun tamamen Labirent ve Şeytan Yerleşkesi odaklıdır).
* Karmaşık diyalog seçim ağaçları (NPC'ler sadece görev vermek, hikaye anlatmak ve ticaret yapmak içindir).
