# Unity_2D_CandyCrane
캔디크레인 안드로이드 앱 제작 및 구글플레이스토어 출시, 구글 애드몹 사용    
3라인 퍼즐과 유사하지만 다른 방식으로 해결해야 하는 퍼즐형 게임으로 직접 기획하고 구현했습니다.   

### Play Demo [FullVideo](https://youtu.be/d8S2b3hhZXc)   
각 열을 클릭하면 해당 열의 가장 윗 캔디가 슬롯으로 떨어집니다.   
슬롯의 캔디가 3개 이상 연속되면 폭탄을 눌러 터트릴 수 있습니다.   
사용하지 못할 캔디를 미리 슬롯에 담아두고, 캔버스에 생성된 모든 캔디를 제거하는 지능형 게임입니다.   
아이템을 사용할 수 있습니다.(거울 아이템 : 최근 슬롯에 추가된 캔디를 복사해 슬롯에 추가합니다.)    <br>
<img width="60%" src="https://user-images.githubusercontent.com/82865325/146879092-5aa58104-22a5-4dbf-be3d-1aeeb21ae9ab.gif">

현재 세가지 모드로 구성되어 있습니다.   

- 클래식 모드   
  - 1~3까지의 난이도를 선택할 수 있으며 최초 맵에 생성된 모든 캔디를 제거하는 규칙   
- 무한 모드   
  - 캔디를 하나 제거할 때 마다 같은 행의 마지막 줄에서 새로운 캔디가 나와 무제한으로 게임을 즐기는 규칙   
- 타임어택 모드   
  - 최초 적은 캔디를 가지고, 일정 시간마다 캔디가 한 줄 씩 늘어나며 빠르게 제거하고, 오랜 시간 버티는 규칙   


3개 이상의 캔디를 모으고, 전부 파괴하세요!   


<img width="5%" src="https://user-images.githubusercontent.com/82865325/172285082-3fbeb8b9-af0b-4171-b64a-576a55b5075c.png"> Download in Playstore : [캔디 크레인](https://play.google.com/store/apps/details?id=com.UngCompany.CandyCrane)   <br><br>

### Requirments
유니티 버전 :  2019.2.21f1   
안드로이드 빌드 : 4.4 이상

