import React, { Component } from 'react';
import './App.css';
import NavBar from "./components/NavBar"
import StationList from "./components/StationList"
import Processes from "./components/Processes"
import axios from 'axios';

class App extends Component {

  constructor(props) {
    super(props)

    this.state = {
      occurrence: []
    }
  }

  render() {
    const AppName = 'Interface de Monitoramento de Processos'
    return (
      <div className="container">
        <NavBar AppName={AppName} />
        <br />
        <div className="row">
          <div className="col-6">
            <StationList station={this.getProcesses.bind(this)} />
          </div>
          <div className="col-6">
            <Processes allData={this.state.occurrence} />
          </div>
        </div>

      </div>
    );
  }

  getProcesses = (k) => {
    axios.get('http://localhost:53468/api/StationProcesses/' + k)
      .then((res) => {
        const occurrence = res.data; 
        this.setState({ occurrence })
      })
  }
}

export default App;
