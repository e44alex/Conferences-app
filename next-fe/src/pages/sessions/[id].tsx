import { Session } from "@/shared/types/Conference";
import useSWR from "swr";
import apiService from "@/shared/service/api";
import { useRouter } from "next/router";
import Link from "next/link";

import "bootstrap/dist/css/bootstrap.css";

const SessionDetails = () => {
  const { query, back } = useRouter();
  const {
    data: session,
    error,
    isLoading,
  } = useSWR<Session>(`/`, () =>
    apiService.getSession(query.id as unknown as number)
  );

  return (
    <>
      <ol className="breadcrumb">
        <li>
          <Link onClick={back} href="">
            Back
            <br />
          </Link>
        </li>
        <li>
          <Link href="/sessions">Agenda</Link>
        </li>
      </ol>

      {error && <p>{error.error}</p>}
      {isLoading && <p>Loading...</p>}

      {session && (
        <div className="session-details">
          <h1>{session.title}</h1>
          <span className="label label-default">{session.track?.name}</span>

          {!error &&
            !isLoading &&
            session.speakers?.map((speaker) => (
              <p key={speaker.id}>
                <em>
                  <Link href="/speakers/[id]" as={`/speakers/${speaker.id}`}>
                    {speaker.name}
                  </Link>
                </em>
              </p>
            ))}

          <p>{session.abstract}</p>
        </div>
      )}
    </>
  );
};

export default SessionDetails;
