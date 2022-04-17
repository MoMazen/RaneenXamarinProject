const mongoose = require('mongoose');

const CategorySchema = new mongoose.Schema({
  //id will be set by default
  name: {
    type: mongoose.Schema.Types.String,
    required: true,
  },
  image: {
    type: mongoose.Schema.Types.String,
    required: true,
  },
  productList: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'Product',
  },
});

const Category = mongoose.model('Category', CategorySchema);

module.exports = { Category, CategorySchema };
