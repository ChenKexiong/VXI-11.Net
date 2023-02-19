# VXI-11.Net
VXI-11.NET はクラスルームでの学習を対象とする、VXI-11通信ソフトです。VXI-11.Net は the GNU General Public License version2のもとで公開されます。

## VXI-11.Netの特徴
- 測定器用のの通信ライブラリから、ソケットプログラムまでを繋げる
- サーバとクライアントの両方を実装
- .NET 6(LTS) に対応し、Windows, Linux, で動作可能
- エラー処理の記述を最小限にする。少ないソース行数で、VXI-11の動作を全て実装する。
- ファイルや関数の数を少なくする。TCP/IPプログラムとの関係を理解しやすくする
- 多くのプログラム言語で使われる記法を用いる。奇抜な簡易記法を使わない。

## 動作方法
- サーバを起動します。
- クライアントを起動します。
  - クライアント側で接続先IPアドレスを入力します。
  - クライアント側でコマンドを入力します
  - サーバ側にコマンドが表示されます
  - サーバ側で応答メッセージを入力します
  - クライアント側に応答メッセージが表示されます