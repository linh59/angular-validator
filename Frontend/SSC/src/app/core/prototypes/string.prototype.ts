export { }

function b64Decode(): string {
  return atob(this);
}

function b64Encode(): string {
  return btoa(this);
}

declare global {

  interface String {
    b64Encode: typeof b64Encode;
    b64Decode: typeof b64Decode;
  }
}
String.prototype.b64Decode = b64Decode;
String.prototype.b64Encode = b64Encode;