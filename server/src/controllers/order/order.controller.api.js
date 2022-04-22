const { request, response } = require('express');

const {
  addOrderService,
  getOrderService,
} = require('../../services/order/order.service.js');

/**
 *
 * @param {request} req
 * @param {response} res
 * @param {Function} next
 */
module.exports.clientGetOrderControllerApi = async function (req, res, next) {
  try {
    const userOrders = await getOrderService(req);
    //if all are ok
    res.status(200).json({
      success: true,
      data: userOrders,
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      error: error,
    });
  }
};
/**
 *
 * @param {request} req
 * @param {response} res
 * @param {Function} next
 */
module.exports.clientAddOrderControllerApi = async function (req, res, next) {
  try {
    const { products } = req.body;

    const result = await addOrderService(req, products);

    //if all are ok

    res.status(200).json({
      success: true,
      data: result,
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      error: error,
    });
  }
};
