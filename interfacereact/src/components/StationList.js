import React, { Component } from 'react';
import axios from 'axios';

class StationList extends Component {

    constructor(props) {
        super(props)

        this.state = {
            stations: []
        }

        this.updateStation = this.updateStation.bind(this);
    }

    getStations() {
        axios.get('http://localhost:53468/api/Station')
            .then((res) => {
                const stations = res.data;
                this.setState({ stations })
            })
    }

    updateStation(event) {
        if (event.target.type === "checkbox") {

            const ochk = event.target.checked

            axios.put(`http://localhost:53468/api/Station/${event.target.name}`,
                {
                    is_active: ochk
                })
                .then((res) => {
                    //console.log(res)
                })
                .catch((error) => {
                    console.log("Erro: " + error);
                })
        }
    }

    componentWillMount() {
        this.getStations();
    }

    render() {
        return (
            <ul className="list-group">
                {this.state.stations.map((s) =>
                    <li key={s.id} className="list-group-item d-flex justify-content-between align-items-center">
                        <form>
                            <div className="form-group form-check">
                                <input type="checkbox" defaultChecked={s.is_active} onChange={this.updateStation} className="form-check-input" name={s.id} />
                            </div>
                        </form>&nbsp;&nbsp;
                        <a className="list-group-item list-group-item-action" onClick={() => this.props.station(s.id)} id="list-profile-list" data-toggle="list" href="#list-stations" role="tab">{s.company_name}<hr />CNPJ: {s.cnpj}</a>
                    </li>
                )}
            </ul>
        )
    }
}

export default StationList;