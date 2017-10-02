# OrmLite LeftJoin Tuple example

This is a reproduction for [Stack Overflow question #46534884][so]

[so]: https://stackoverflow.com/q/46534884

## Expected output

```js
    [
      {
        "Item1": {
          "OrderId": 1,
          "Name": "Alpha"
        },
        "Item2": null
      }
    ]
```

## Actual output

```js
[
  {
    "Item1": {
      "OrderId": 1,
      "Name": "Alpha"
    },
    "Item2": {
      "LineItemId": 0,
      "OrderId": 0
    }
  }
]
```
