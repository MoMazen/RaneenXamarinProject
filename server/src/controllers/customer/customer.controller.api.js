const { response, request } = require('express');
const Address = require('../../../models/Address.js');
/**
 *
 * @param {request} req
 * @param {response} res
 * @param {Function} next
 */
module.exports.customerProfileControllerApi = async function (req, res, next) {
  try {
    if (req.user) {
      const userAddress = await Address.findOne({
        customerId: req.user._id,
      });
      if (userAddress) {
        req.user.address = userAddress;
        res.status(200).json({
          success: true,
          data: req.user,
        });
      } else {
        req.user.address = null;
        res.status(200).json({
          success: true,
          data: req.user,
        });
      }
    } else {
      throw new Error('internal server error');
    }
  } catch (error) {
    res.status(500).json({
      success: false,
      error: error.message,
    });
  }
};
