const mongoose = require('mongoose');

// const Customer = require('./Customer.js');

// const Product = require("./Product.js");
const OrderSchema = new mongoose.Schema(
  {
    customer: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'Customer',
      required: true,
    },
    products: {
      type: [
        {
          id: {
            type: mongoose.Schema.Types.ObjectId,
            ref: 'Product',
          },
          amount: {
            type: mongoose.Schema.Types.Number,
            // required: true,
          },
        },
      ],
      default: [],
    },
    TotalPrice: {
      type: mongoose.Schema.Types.Number,
      // required: true,
      default: 0,
    },
  },
  {
    writeConcern: {
      w: 'majority',
      j: true,
      wtimeout: 1000,
    },
  }
);

const Order = mongoose.model('Order', OrderSchema);

module.exports = Order;
