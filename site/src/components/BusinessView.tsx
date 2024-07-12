import React from "react";
import fetchBusinesses, { Business } from "../api/BusinessClient";

type BusinessViewState = {
    businesses: Business[]
}

export default class BusinessView extends React.Component<{}, BusinessViewState> {
    constructor(props: {}) {
        super(props);

        this.state = {businesses: []};
    }

    componentDidMount() {
        fetchBusinesses().then(businesses => this.setState({ businesses: businesses }))
    }

    render() {
        return (
            <div>
                {this.state.businesses.map(b => {
                    return <p key={b.id}>{b.name}</p>
                })}
            </div>
        )
    }
}