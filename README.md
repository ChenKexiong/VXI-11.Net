# VXI-11.Net
VXI-11.NET はクラスルームでの学習を対象とする、VXI-11通信ソフトです。VXI-11.Net は the GNU General Public License version2のもとで公開されます。

## VXI-11.Netの特徴
- VXI-11 サーバとVXI-11 クライアントを実装する。
- .NET 6(LTS) に対応し、標準的な Windows, Linux 環境で動作する。
- VISAライブラリのAPI から ソケットプログラムまでを繋げる
- エラー処理の記述を最小限にし、少ないソースコード行数でVXI-11の機能を実装する。
- ファイルの数や関数の呼び出し階層を少なくする。ソケットプログラムとの関係を理解しやすくする
- C言語(C89)で使われる記法を用いる。奇抜な簡易記法を使わない。
 
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
説明は簡潔で1ページを2000文字以内にします。
