const { request, response } = require('express');

const {
  addProductsService,
  getAllProductsService,
} = require('../../services/product/product.services.js');

/**
 *
 * @param {request} req
 * @param {response} res
 *
 */
module.exports.addProductControllerApi = async function (req, res, next) {
  try {
    const product = await addProductsService(req);

    res.status(200).json({
      success: true,
      data: product,
    });
  } catch (error) {
    res.status(200).json({
      success: false,
      error: { message: error.message },
    });
  }
};

/**
 *
 * @param {request} req
 * @param {response} res
 *
 */
module.exports.getAllProductsControllerApi = async function (req, res, next) {
  try {
    const products = await getAllProductsService();
    res.status(200).json({
      success: true,
      data: products,
    });
  } catch (error) {
    res.status(200).json({
      success: false,
      error: { message: error.message },
    });
  }
};
