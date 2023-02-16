# VXI-11.Net
VXI-11.NET はプログラム初心者向けのクラスルームでの学習を対象とするVXI-11通信ソフトです。VXI-11.Net は the GNU General Public License version2のもとで公開されます。

## VXI-11.Netの特徴
- VXI-11 サーバとVXI-11 クライアントを実現します。
- Windows と Linux を動作環境とします。
- NET 6(C# 7.3)でプログラムを作成します。
- VISA ライブラリの API からソケットプログラムまで実装します。
- エラー処理を可能な限り省略し、少ないソースコード行数で VXI-11 の機能を実装します。
- ファイルの数や関数の呼び出し階層を少なくします。1関数の大きさは100行未満を目安とします。
- 奇抜な記法を使わず、C言語(C99)の記法を目安とします。
 
## 動作方法
- サーバを起動します。
- クライアントを起動します。
  - クライアント側で接続先IPアドレスを入力します。
  - クライアント側でコマンドを入力します
  - サーバ側にコマンドが表示されます
  - サーバ側で応答メッセージを入力します
  - クライアント側に応答メッセージが表示されます

## 標準開発環境
無料で入手でき商用利用の制限がなく、シェアの高いツールを標準開発環境とします。
- OS：Windows 11（有償ソフト）
- コンパイラ：Net 6 SDK
- リポジトリ：GitHub
- 構成管理ツール：Git for Windows, TortoiseGit
- エディタ：Visual Studio Code
- CI/CD：GitHub Actions

## 品質目標
- ソースコードの記法について C# コーディングガイドを尊重します。
- Github Actions を使って、Win, Linux, ホスト上でビルドを確認します
- テストでは模擬　VXI-11 サーバを使用し、上記フィーチャーの動作を確認します。
- テストカバレッジは正常動作パスのカバレッジ 100%、異常動作パスのカバレッジ0%をリリース指標とします。

## 文書目標
説明は簡潔にとどめ 1 ファイルのサイズは 4.0KB 以内を目安にします。
