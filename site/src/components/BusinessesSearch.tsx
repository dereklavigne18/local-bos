import BusinessHooks from 'src/hooks/useBusinesses';

export default function BusinessesSearch() {
    const { businesses } = BusinessHooks.useBusinesses();
    return (
        <>
            <b>Businesses</b>
            {businesses.map(b => {
                return <p key={b.id}>{b.name}</p>
            })}
        </>
    )
};