Projemde Backend için .NET , UI kısmi için Angular ve Database olarak PostgreSQL kullandım.
Backend için Onion Architecture ile yapıyı kurdum.CQRS yapısı mikroservis yapısına geçebilecek yapıda projeye eklendi. Mediator Pattern ile CQRS'i gerçekleştirdim.
İdentity Mekanizması'yla birlikte JWT Web Token ile kimlik yönetimi sağladım.Standart giriş şekli harici Google ile giriş yapma işlemini ekledim
<img width="1363" height="337" alt="image" src="https://github.com/user-attachments/assets/8824ec77-6088-47b6-9bd7-04c6d2d2c295" />


Loglama işlemlerini gerçekleştirdin ve Seq ile görselleştirerek gözlemledim.
Gerçek zamanlı iletişim için SignalR yapısını projeye dahil etdim
Mail gönderme işlemlerini gerçekleştirdim.
Şifre yenileme işlemini kullanıcıya bağlantının olduğu mail göndererek yenilemesini sağladım.



<img width="608" height="196" alt="image" src="https://github.com/user-attachments/assets/e101ee84-4295-4af0-b0f7-95ddc29f6099" />
<img width="1102" height="503" alt="image" src="https://github.com/user-attachments/assets/3b7a0f70-20c0-4b9d-9492-cd46e893e7a1" />


Role Based Access Control mimarisini oluşturdum.Endpointlere roller atayarak kullanıcılarla rolleri ilişkilendirdim.


<img width="1355" height="706" alt="image" src="https://github.com/user-attachments/assets/96b7f027-8a3b-4378-9212-ea82e09f4046" />
<img width="1287" height="603" alt="image" src="https://github.com/user-attachments/assets/7f5b2dd8-5093-4e59-8afa-2d0131b9b9ff" />
<img width="976" height="416" alt="image" src="https://github.com/user-attachments/assets/7cd3db46-bd4d-41e2-9056-41081dcd194c" />




Ürünler Listelendi-Ürün Filtreleme ve Arama İşlemleri Yapıldı


<img width="1388" height="867" alt="image" src="https://github.com/user-attachments/assets/d9395830-1440-409b-8994-ba2f16d2eab9" />
<img width="1667" height="823" alt="image" src="https://github.com/user-attachments/assets/0a9d8909-6f24-4ac4-943e-6a10db83c9c5" />




Ürün listeleme,arama,ekleme,silme,güncelleme,fotoğraf yükleme alanları ve qr okumayla stok güncelleme alanları oluşturuldu



<img width="996" height="612" alt="image" src="https://github.com/user-attachments/assets/4d0730de-a076-4659-bb1a-07e1a5ef73d5" />
<img width="1245" height="728" alt="image" src="https://github.com/user-attachments/assets/26ed533c-8c9c-4063-bc96-bf5d73416e05" />
<img width="1292" height="791" alt="image" src="https://github.com/user-attachments/assets/a409379c-2196-45e2-b0e2-eb7ab0653de5" />





Ürünler için QR oluşturma işlemini projeye dahil ettim. Ayrıca QR kod ile stok güncelleme işleminide projemde kullandım

<img width="953" height="712" alt="image" src="https://github.com/user-attachments/assets/7963fba8-2509-4d12-97b4-68541f45ab2a" />


Kategori Ekleme-Silme-Güncelleme İşlemleri yapildi


<img width="1336" height="822" alt="image" src="https://github.com/user-attachments/assets/a33e97b3-f700-4575-a899-4d6149f9b81c" />



Sipariş listeleme 

<img width="1335" height="790" alt="image" src="https://github.com/user-attachments/assets/299c6573-0c4e-44aa-94d8-7cddc5e49692" />




Sipariş Detayları

<img width="1271" height="610" alt="image" src="https://github.com/user-attachments/assets/7f295e7b-d9e7-4b76-a4b4-f00f85da3e10" />



Kullanıcı Listeleme

<img width="737" height="646" alt="image" src="https://github.com/user-attachments/assets/204f41eb-96dd-4dbc-9ac3-7009eb72cc29" />



Kullanıcıya Rol Atama

<img width="1240" height="482" alt="image" src="https://github.com/user-attachments/assets/432d0889-c8c1-439a-8bac-59d1ba73798e" />



Dosya yükleme işlemi için iki farklı yapı tasarlandı. Local Storage ve Azure Blob servisleri ile iki farklı dosya yükleme servisi geliştirdim.Projemde Azure üzerinden dosya yüklemeyi kullanıyorum.Ama değişime açık olacak şekilde geliştirildiği için Local Storage veya farklı dosya yükleme yapıları aktif projeye dahil edilebilir yapıda.

<img width="1216" height="537" alt="image" src="https://github.com/user-attachments/assets/4e3aefa1-50ee-42cd-a6d7-214cd98436d6" />


Dashboard Alanında Aylık Satışla İlgili  Grafik 


<img width="1347" height="805" alt="image" src="https://github.com/user-attachments/assets/51f1a8a6-9a54-45e7-bb7f-486f3b985a74" />


Müşteri listeleme,arama ve müşteri siparişlerini inceleme alanları
<img width="1336" height="802" alt="image" src="https://github.com/user-attachments/assets/e9026854-b79c-4edd-99c5-a45a68e64345" />



Sepet


<img width="1296" height="481" alt="image" src="https://github.com/user-attachments/assets/995a03e8-35e9-4c2c-8cad-ea6701d4a8d9" />


Client kısmında tasarım için Material ve Bootstrap kullandım.
Bir çok kütüphaneyi projemde kullandım(Toastr,Alertify,SignalR,Spinner,Google,File Upload...).
Kütüphaneler kullanılırken bazıları özelleştirildi.Http istekleri service de gerçeleştirildi.Bu şekilde kod tekrarı önlendi.
Delete işlemi için direktif olusturuldu.Birden fazla kısımda delete işleminin aynı yapılmasından kaynaklı direktif şeklinde tasarlandı.
Dialog işlemi için servis oluşturuldu.
Dinamik alışveriş sepeti oluşturuldu.Bunun Dynamic Component Loadin kullanıldı.




