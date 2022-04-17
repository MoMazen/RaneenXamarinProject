//E-commerce application .

const DBEvents = require('../../config/DBEvents.js');

class ServerApp {
  constructor() {
    this.express = require('express');

    this.cors = require('cors');

    this.mongodbConnectionManager = require('../../config/db.config.js');

    this.app = null;
  }

  async init() {
    //connection to the db

    await this.dbInitialization();

    if (this.app === null) {
      const PORT = Number.parseInt(process.env.PORT) || 4000;

      this.app = this.express();

      this.app.use(this.cors()); //for mobile

      this.app.use(this.express.json());

      this.applicationRoutes();

      this.app.listen(PORT, () => {
        console.log(`server is on and listening on PORT : ${PORT}`);
      });
    }
  }

  //application routes
  applicationRoutes() {
    //auth
    this.app.use('/app', require('../routes/auth/auth.routes.js'));

    //client routes
    this.app.use('/app', require('../routes/Client/test.js'));

    this.app.get('/', (req, res) => {
      res.status(200).json({
        success: true,
        data: 'hello world from server app',
      });
    });
  }

  //db initialization
  async dbInitialization() {
    //registering db events

    this.mongodbConnectionManager.on(
      DBEvents.connectionState,
      connectionState => {
        console.log(connectionState);
      }
    );

    //handling connection errors
    this.mongodbConnectionManager.on(DBEvents.connectionError, error => {
      console.log(error); //logging the connection errors
      //TODO retry to connect again to the db in case of error
      //TODO make a worker to keep retrying to connect to the db in case of connection failure
    });

    await this.mongodbConnectionManager.init();
  }
}

module.exports = ServerApp;
