const mongoose = require('mongoose');

const productStatus = require('./productStatus.js');

const ProductSchema = new mongoose.Schema({
  //id will be set by default

  title: {
    type: mongoose.Schema.Types.String,
    required: true,
  },
  description: {
    type: mongoose.Schema.Types.String,
    required: true,
  },
  vendor: {
    type: mongoose.Schema.Types.String,
    required: true,
  },
  createdAt: {
    type: mongoose.Schema.Types.Date,
    default: Date.now,
  },
  updatedAt: {
    type: mongoose.Schema.Types.Date,
    required: Date.now,
  },
  publishedAt: {
    type: mongoose.Schema.Types.Date,
    default: Date.now,
  },
  status: {
    type: mongoose.Schema.Types.String,
    enum: [productStatus.available, productStatus.outOfStock], //TODO clean this hard coded value
    required: true,
  },
  tags: [
    {
      type: mongoose.Schema.Types.String,
    },
  ],
  weight: {
    type: mongoose.Schema.Types.String,
    // required: true,
  },
  weight_unit: {
    type: mongoose.Schema.Types.String,
    // required: true,
  },
  price: {
    type: mongoose.Schema.Types.Number,
    required: true,
  },
  amount: {
    type: mongoose.Schema.Types.number,
    // required: true,
  },
  images: [{ type: mongoose.Schema.Types.String }],
});

const Product = mongoose.model('Product', ProductSchema);

module.exports = Product;
