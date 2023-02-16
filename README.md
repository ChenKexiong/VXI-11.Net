# VXI-11.Net
VXI-11.NET はクラスルームでの学習を対象とする、VXI-11通信ソフトです。VXI-11.Net は the GNU General Public License version2のもとで公開されます。

## VXI-11.Netの特徴
- Windows と Linux を動作環境とする。NET 6(C# 7.3)でプログラムを作成する。
- VXI-11 サーバとVXI-11 クライアントを実現する。
- VISA ライブラリの API からソケットプログラムまで実装する
- エラー処理の記述を省略し、少ないソースコード行数で VXI-11 の機能を実装する。
- プログラム初心者を対象とし、ファイルの数や関数の呼び出し階層を少なくする。
- プログラム初心者を対象とし、奇抜な簡易記法を使わない。C言語(C99)の記法を目安とする。
 
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
