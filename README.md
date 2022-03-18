# Destiny2-Oath-of-the-disciple
Destiny2 門弟の誓い コールアシスト

## 【使い方】
### フルスクリーン
1. 「Destiny2 Oath of the disciple.exe」起動
2. シンボルを右クリック
3. 「スマートフォンサーバー起動」を選択
  - ファイアーウォールの許可をしないと、スマートフォンとの連携が取れません
5. スマートフォンで表示されているQRコードを読み込み、指定アドレスを開く
  1. 画面上部「Destiny2 Oath of the disciple CONNECTED」と表示されていれば、接続成功
  2. 画面上部「Destiny2 Oath of the disciple DISCONNECTED」と表示されていれば、PCとスマートフォンの連携に失敗しています。
    - PC操作：「Page Up」キーを押し、手順2からやり直す
6. Destiny2を起動
7. スマートフォンのシンボルをタップし、Destiny2側にシンボルの名称が送信されているか確認

### ブラウザ
1. 「Destiny2 Oath of the disciple.exe」起動
2. 「Page Up」キーを押し一時非表示に
3. Destiny2を起動
4. 「Page Up」キーを押し再表示
5. シンボルをクリック
6. Destiny2側にシンボルの名称が送信されているか確認

## 【取り扱い説明】
1. Page Upキーでアプリの表示、非表示
3. Skaypeなどのアプリが起動中は使用できないかもしれません（80ポート、8080ポート仕様）
4. 「ソースコードのポート番号変えたのに、繋がらない！！」そこまで考えて作っていません…以下の方法で接続が可能と思われます。
  1. 「root.bat」の編集
    - 「httpServer.cs」のポート番号を編集した場合 
      - 「netsh http add urlacl url=http://+:80/ user=everyone」の「80」がポート番号になります。
      - 「QRコード」から読み取ったアドレス「http://192.168.*.*/index.html」を「http://192.168.*.*:8888/index.html」のように変更したポート番号を指定してください。
    - 「Client.cs」のポート番号を編集した場合
      - 「netsh advfirewall firewall set rule name="Destiny2 Oath of the disciple" new program=system profile=private protocol=tcp localport=80」の「80」がポート番号になります。

## 【動作環境】
- Windows 10, Windows 11
  - Microsoft Edge
  - Google Chrome
  - Opera
- Android 11
  - Google Chrome
  - Opera
- iPhone
  - Google Chrome
  - Opera
  - Yahoo ブラウザ
  - Safari
