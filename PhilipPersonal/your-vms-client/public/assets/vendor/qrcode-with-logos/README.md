# qrcode-with-logos

![QRcode-with-logos](https://github.com/zxpsuper/qrcode-with-logos/raw/master/demo.png)

## Introduction

QRcode-with-logos is a tool for creating a QRcode with a logo in your object.

It depend on `qrcode` dependence and be more powerful than `qrcode`.

It can create a canvas or a image for the QRcode and even you can use the method to download the file you want.

## Usage

- Install the module

```
npm install qrcode-with-logos --save
```

## Options

### `content`(required)

Type: `String`

QR Code content.

### `width`

Type: `Number`

Default: `0`

QR Code width.

### `canvas`

Type: `DomElement`

Default: a new canvas tag

A canvas tag to show the QR code.

### `image`

Type: `DomElement`

Default: a new img tag

A img tag to show the QR code.

### `download`

Type: `Boolean`

Default: `false`

You can set the value to be true to download the file immediately, the premise is that you have use the `toImage()` methods.

Examplate

```js
new QrCodeWithLogo({
  canvas: document.getElementById("canvas"),
  content: "https://github.com/zxpsuper",
  width: 380,
  download: true,
  image: document.getElementById("image"),
  logo: {
    src: "https://avatars1.githubusercontent.com/u/28730619?s=460&v=4"
  }
}).toImage();
```

### `downloadName`

Type: `String`

Default: `qr-code.png`

Set the download file name, should be used with the `download` property.

### `nodeQrCodeOptions`

Type: `Object`

`nodeQrCodeOptions.margin`:

Type `Number` -- qrcode margin

`nodeQrCodeOptions.errorCorrectionLevel`:

Type `String`-- qrcode errorCorrectionLevel, such as "M", "Q", "H"

`nodeQrCodeOptions.scale`:

Type `Number`-- qrcode scale

`nodeQrCodeOptions.color`:

Type `Object`-- qrcode color

`nodeQrCodeOptions.color.dark`:

Type `String`-- qrcode color value of dark

`nodeQrCodeOptions.color.light`:

Type `String`-- qrcode color value of light

### `logo`

Type: `Object`

`logo.src`(required):

Type `String`-- logo url or logo module

`logo.logoRadius`:

Type `Number` -- Default: `0`

`logo.logoSize`:

Type `Number` -- Default: `0.15`, is the scale to qrcode

`logo.borderRadius`:

Type `Number` -- Default: `8`

`logo.borderColor`:

Type `String` -- Default: `"#ffffff"`

`logo.borderSize`:

Type `Number` -- Default: `0.05` , is the scale to qrcode

`logo.bgColor`:

Type `String` -- Default: `"#ffffff"`, the logo background color

`logo.crossOrigin`:

Type `String` -- Default: `"Anonymous"`

## Methods

`toCanvas()` ———— Return `Promise`, show the qrcode by canvas tag

```js
new QrCodeWithLogo({
  canvas: document.getElementById("canvas"),
  content: "https://github.com/zxpsuper",
  width: 380,
  //   download: true,
  image: document.getElementById("image"),
  logo: {
    src: "https://avatars1.githubusercontent.com/u/28730619?s=460&v=4"
  }
})
  .toCanvas()
  .then(() => {});
```

`toImage()` ———— Return `Promise`, show the qrcode by img tag

```js
new QrCodeWithLogo({
  canvas: document.getElementById("canvas"),
  content: "https://github.com/zxpsuper",
  width: 380,
  //   download: true,
  image: document.getElementById("image"),
  logo: {
    src: "https://avatars1.githubusercontent.com/u/28730619?s=460&v=4"
  }
})
  .toImage()
  .then(() => {});
```

`downloadImage(name: string)` ———— Return `Promise`, set the filename and download the image, should be use after `toImage()` method.

```js
let qrcode = new QrCodeWithLogo({
  canvas: document.getElementById("canvas"),
  content: "https://github.com/zxpsuper",
  width: 380,
  //   download: true,
  image: document.getElementById("image"),
  logo: {
    src: "https://avatars1.githubusercontent.com/u/28730619?s=460&v=4"
  }
});

qrcode.toImage().then(() => {
  setTimeout(() => {
    qrcode.downloadImage("hello world");
  }, 2000);
});
```

## Example

```html
<canvas id="canvas"></canvas> <img src="" alt="" id="image" />
```

```js
import QrCodeWithLogo from "qrcode-with-logos";
let qrcode = new QrCodeWithLogo({
  canvas: document.getElementById("canvas"),
  content: "https://github.com/zxpsuper",
  width: 380,
  //   download: true,
  image: document.getElementById("image"),
  logo: {
    src: "https://avatars1.githubusercontent.com/u/28730619?s=460&v=4"
  }
});

qrcode.toCanvas().then(() => {
  qrcode.toImage().then(() => {
    setTimeout(() => {
      qrcode.downloadImage("hello world");
    }, 2000);
  });
});
```

## Ecosystem

| Project  | Status                             | Description                   |
| -------- | ---------------------------------- | ----------------------------- |
| [qrcode] | [![qrcode-status]][qrcode-package] | QR code/2d barcode generator. |

[qrcode]: https://github.com/soldair/node-qrcode
[qrcode-status]: https://img.shields.io/npm/v/qrcode.svg
[qrcode-package]: https://npmjs.com/package/qrcode

## Questions or advise

If you have some question or advise, you can send me a E-mail(zxpscau@163.com).
